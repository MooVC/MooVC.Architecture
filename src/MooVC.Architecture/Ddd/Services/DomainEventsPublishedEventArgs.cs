namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;

    public sealed class DomainEventsPublishedEventArgs
        : DomainEventsEventArgs
    {
        public DomainEventsPublishedEventArgs(IEnumerable<DomainEvent> events)
            : base(events)
        {
        }
    }
}