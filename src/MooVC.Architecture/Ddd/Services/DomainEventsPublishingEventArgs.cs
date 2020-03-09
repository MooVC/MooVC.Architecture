namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;

    public sealed class DomainEventsPublishingEventArgs
        : DomainEventsEventArgs
    {
        public DomainEventsPublishingEventArgs(IEnumerable<DomainEvent> events)
            : base(events)
        {
        }
    }
}