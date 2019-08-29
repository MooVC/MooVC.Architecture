namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using Collections.Generic;
    using static System.String;
    using static Resources;

    [Serializable]
    public abstract class EventCentricAggregateRoot
        : AggregateRoot
    {
        public const string HandlerName = "Handle";

        private readonly List<DomainEvent> changes;

        protected EventCentricAggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id, version)
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
            if (!(Version == DefaultVersion && changes.Count == 0))
            {
                throw new InvalidOperationException(Format(
                    EventCentricAggregateRootDomainEventHandlerNotSupportedException, 
                    Id,
                    Version,
                    GetType().Name));
            }

            history.ForEach(@event => ApplyChange(() => @event, isNew: false));

            Version = history
                .Select(@event => @event.Aggregate.Version)
                .DefaultIfEmpty(DefaultVersion)
                .Max();

            MarkChangesAsCommitted();
        }

        public override void MarkChangesAsCommitted()
        {
            if (HasUncommittedChanges)
            {
                changes.Clear();

                base.MarkChangesAsCommitted();
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
    }
}