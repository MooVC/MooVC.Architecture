namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;
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
            changes = info.TryGetInternalValue(nameof(changes), defaultValue: new List<DomainEvent>());
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (changes.Any())
            {
                info.AddInternalValue(nameof(changes), changes);
            }
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return changes.ToArray();
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            if (history.Any())
            {
                if (changes.Any())
                {
                    throw new AggregateHasUncommittedChangesException(this.ToVersionedReference());
                }

                IEnumerable<DomainEvent> sequence = history
                    .OrderBy(@event => @event.Aggregate.Version)
                    .ToArray();

                if (!sequence.SequenceEqual(history))
                {
                    throw new AggregateEventSequenceUnorderedException(this.ToVersionedReference(), history);
                }

                VersionedReference mismatch = sequence
                    .Where(@event => @event.Aggregate.Id != Id)
                    .Select(@event => @event.Aggregate)
                    .FirstOrDefault();

                if (mismatch is { })
                {
                    throw new AggregateEventMismatchException(this.ToVersionedReference(), mismatch);
                }

                SignedVersion startingVersion = sequence.First().Aggregate.Version;

                if (!(Version.IsNew || startingVersion.IsNext(Version)))
                {
                    throw new AggregateHistoryInvalidForStateException(this.ToVersionedReference(), sequence, startingVersion);
                }

                sequence.ForEach(@event => ApplyChange(() => @event, isNew: false));

                State = new AggregateState(sequence.Last().Aggregate.Version);
            }
        }

        public override void MarkChangesAsCommitted()
        {
            if (HasUncommittedChanges)
            {
                base.MarkChangesAsCommitted();

                changes.Clear();
            }
        }

        protected void ApplyChange<TEvent>(Func<TEvent> change, Action<TEvent> handler = default, bool isNew = true)
            where TEvent : DomainEvent
        {
            bool triggersVersionIncrement = isNew && !HasUncommittedChanges;

            if (triggersVersionIncrement)
            {
                MarkChangesAsUncommitted();
            }

            try
            {
                TEvent @event = change();

                handler ??= CreateHandler<TEvent>(@event.GetType());

                handler(@event);

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

        protected override void OnChangesMarkedAsCommitted(EventArgs args = default)
        {
            base.OnChangesMarkedAsCommitted(new ChangesMarkedAsCommittedEventArgs(changes));
        }

        private Action<TEvent> CreateHandler<TEvent>(Type eventType)
            where TEvent : DomainEvent
        {
            Type type = GetType();

            MethodInfo handler = type.GetMethod(
                HandlerName,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { eventType },
                null);

            if (handler is null)
            {
                throw new NotSupportedException(Format(
                    EventCentricAggregateRootDomainEventHandlerNotSupportedException,
                    eventType.Name,
                    type.Name));
            }

            return @event => _ = handler.Invoke(this, new object[] { @event });
        }
    }
}