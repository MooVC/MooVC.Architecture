namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class DomainEventsPublishedEventArgs
        : DomainEventsEventArgs
    {
        public DomainEventsPublishedEventArgs(IEnumerable<DomainEvent> events)
            : base(events)
        {
        }

        private DomainEventsPublishedEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}