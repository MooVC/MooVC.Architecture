namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using Collections.Generic;
    using static Resources;

    [Serializable]
    public abstract class EventCentricAggregateRoot
        : AggregateRoot
    {
        public const string HandlerName = "Handle";

        private readonly List<DomainEvent> changes = new List<DomainEvent>();

        protected EventCentricAggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id, version)
        {
        }

        protected EventCentricAggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            var changes = (DomainEvent[])info.GetValue(nameof(this.changes), typeof(DomainEvent[]));

            this.changes.AddRange(changes);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(changes), changes.ToArray());
        }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            return changes;
        }

        public void LoadFromHistory(IEnumerable<DomainEvent> history)
        {
            history.ForEach(@event => ApplyChange(@event, isNew: false));

            Version = history
                .Select(@event => @event.Version)
                .DefaultIfEmpty(DefaultVersion)
                .Max() + 1;
        }

        public override void MarkChangesAsCommitted()
        {
            base.MarkChangesAsCommitted();

            changes.Clear();
        }

        protected void ApplyChange(DomainEvent @event, bool isNew = true)
        {
            Type type = GetType();
            Type eventType = @event.GetType();

            MethodInfo handler = type
                .GetMethod(HandlerName, BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { eventType }, null);
            
            if (handler == null)
            {
                throw new NotSupportedException(string.Format(
                    DomainEventHandlerNotSupportedException, eventType.Name, type.Name));
            }

            _ = handler.Invoke(this, new object[] { @event });

            if (isNew)
            {
                changes.Add(@event);
            }
        }
    }
}