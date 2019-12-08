namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;

    public abstract class DomainEventsEventArgs
        : EventArgs
    {
        protected DomainEventsEventArgs(IEnumerable<DomainEvent> events)
        {
            Events = events.Snapshot();
        }

        public IEnumerable<DomainEvent> Events { get; }
    }
}