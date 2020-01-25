namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using static System.String;
    using static Resources;

    [Serializable]
    public abstract class EventCentricAggregateRoot
        : AggregateRoot
    {
        public const string HandlerName = "Handle";

        private readonly List<DomainEvent> changes;

        protected EventCentricAggregateRoot(Guid id)
            : base(id)
        {
            changes = new List<DomainEvent>();
        }

        protected EventCentricAggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            changes = (List<DomainEvent>)info.GetValue(nameof(changes), typeof(List<DomainEvent>));
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(changes), changes);
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return changes.ToArray();
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            IEnumerable<DomainEvent> sequence = history
                .OrderBy(@event => @event.Aggregate.Version)
                .ToArray();

            if (!sequence.SequenceEqual(history))
            {
                throw new InvalidOperationException(Format(
                    EventCentricAggregateRootHistorySequenceUnordered,
                    Id,
                    Version,
                    GetType().Name));
            }

            VersionedReference mismatch = sequence
                .Where(@event => !@event.Aggregate.IsMatch(this))
                .Select(@event => @event.Aggregate)
                .FirstOrDefault();

            if (mismatch is { })
            {
                throw new AggregateEventMismatchException(this.ToVersionedReference(), mismatch);
            }

            if (HasUncommittedChanges)
            {
                throw new InvalidOperationException(Format(
                    EventCentricAggregateInvalidStateForLoadFromHistory,
                    Id,
                    Version,
                    GetType().Name));
            }

            history.ForEach(@event => ApplyChange(() => @event, isNew: false));

            State = new AggregateState(sequence.Last().Aggregate.Version);
        }

        public override void MarkChangesAsCommitted()
        {
            if (HasUncommittedChanges)
            {
                base.MarkChangesAsCommitted();

                changes.Clear();
            }
        }

        protected void ApplyChange(Func<DomainEvent> change, bool isNew = true)
        {
            bool triggersVersionIncrement = isNew && !HasUncommittedChanges;

            if (triggersVersionIncrement)
            {
                MarkChangesAsUncommitted();
            }

            try
            {
                DomainEvent @event = change();
                Type type = GetType();
                Type eventType = @event.GetType();

                MethodInfo handler = type
                    .GetMethod(HandlerName, BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);

                if (handler == null)
                {
                    throw new NotSupportedException(Format(
                        EventCentricAggregateRootDomainEventHandlerNotSupportedException,
                        eventType.Name,
                        type.Name));
                }

                _ = handler.Invoke(this, new object[] { @event });

                if (isNew)
                {
                    changes.Add(@event);
                }
            }
            catch
            {
                if (triggersVersionIncrement)
                {
                    RollbackUncommittedChanges();
                }

                throw;
            }
        }

        protected override void OnChangesMarkedAsCommitted(EventArgs e = null)
        {
            base.OnChangesMarkedAsCommitted(new ChangesMarkedAsCommittedEventArgs(changes));
        }
    }
}