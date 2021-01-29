namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;

    [Serializable]
    public abstract class DomainEventsEventArgs
        : EventArgs,
          ISerializable
    {
        protected DomainEventsEventArgs(IEnumerable<DomainEvent> events)
        {
            Events = events.Snapshot();
        }

        protected DomainEventsEventArgs(SerializationInfo info, StreamingContext context)
        {
            Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        }

        public IEnumerable<DomainEvent> Events { get; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddEnumerable(nameof(Events), Events);
        }
    }
}