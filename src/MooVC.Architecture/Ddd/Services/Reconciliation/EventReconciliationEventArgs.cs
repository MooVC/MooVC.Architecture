namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class EventReconciliationEventArgs
    {
        public EventReconciliationEventArgs(IEnumerable<DomainEvent> events)
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