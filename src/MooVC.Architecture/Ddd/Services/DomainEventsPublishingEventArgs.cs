namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class DomainEventsPublishingEventArgs
        : DomainEventsEventArgs
    {
        public DomainEventsPublishingEventArgs(IEnumerable<DomainEvent> events)
            : base(events)
        {
        }

        private DomainEventsPublishingEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}