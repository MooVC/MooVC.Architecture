namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class EventReconciliationEventArgs
    {
        internal EventReconciliationEventArgs(IEnumerable<DomainEvent> events)
        {
            ArgumentIsAcceptable(
                events,
                nameof(events),
                _ => events.SafeAny(),
                EventReconciliationEventArgsEventsRequired);

            Events = events.Snapshot();
        }

        public IEnumerable<DomainEvent> Events { get; }
    }
}