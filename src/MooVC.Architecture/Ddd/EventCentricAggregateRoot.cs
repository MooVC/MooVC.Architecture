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

            _ = info.TryAddInternalValue(nameof(changes), changes, predicate: _ => changes.Any());
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
                    throw new AggregateHasUncommittedChangesException(new VersionedReference(this));
                }

                IEnumerable<DomainEvent> sequence = history
                    .OrderBy(@event => @event.Aggregate.Version)
                    .ToArray();

                if (!sequence.SequenceEqual(history))
                {
                    throw new AggregateEventSequenceUnorderedException(new VersionedReference(this), history);
                }

                VersionedReference mismatch = sequence
                    .Where(@event => @event.Aggregate.Id != Id)
                    .Select(@event => @event.Aggregate)
                    .FirstOrDefault();

                if (mismatch is { })
                {
                    throw new AggregateEventMismatchException(new VersionedReference(this), mismatch);
                }

                SignedVersion startingVersion = sequence.First().Aggregate.Version;

                if (!(startingVersion.IsNext(Version) || (startingVersion.IsNew && Version.IsNew)))
                {
                    throw new AggregateHistoryInvalidForStateException(new VersionedReference(this), sequence, startingVersion);
                }

                sequence.ForEach(@event => ApplyChange(() => @event, isNew: false));

                State = new AggregateState(sequence.Last().Aggregate.Version);
            }
        }

        public sealed override void MarkChangesAsCommitted()
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
                base.MarkChangesAsUncommitted();
            }

            try
            {
                TEvent @event = change();

                handler ??= CreateHandler<TEvent>(@event.GetType());

                if (handler is { })
                {
                    handler(@event);
                }

                if (isNew)
                {
                    changes.Add(@event);
                }
            }
            catch
            {
                if (triggersVersionIncrement)
                {
                    base.RollbackUncommittedChanges();

                    changes.Clear();
                }

                throw;
            }
        }

        protected sealed override void MarkChangesAsUncommitted()
        {
            throw new InvalidOperationException(EventCentricAggregateRootStateChangesDenied);
        }

        protected override void OnChangesMarkedAsCommitted(EventArgs args = default)
        {
            base.OnChangesMarkedAsCommitted(new ChangesMarkedAsCommittedEventArgs(changes));
        }

        protected sealed override void RollbackUncommittedChanges()
        {
            throw new InvalidOperationException(EventCentricAggregateRootStateChangesDenied);
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
                return default;
            }

            return @event => _ = handler.Invoke(this, new object[] { @event });
        }
    }
}