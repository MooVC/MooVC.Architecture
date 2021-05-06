namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class EventReconciler
        : IEventReconciler
    {
        public event EventsReconciledEventHandler? EventsReconciled;

        public event EventsReconcilingEventHandler? EventsReconciling;

        public event EventSequenceAdvancedEventHandler? EventSequenceAdvanced;

        public async Task<ulong?> ReconcileAsync(ulong? previous = default, ulong? target = default)
        {
            bool hasEvents;

            do
            {
                (ulong? lastSequence, IEnumerable<DomainEvent> events) = await
                    GetEventsAsync(
                        previous,
                        target: target)
                    .ConfigureAwait(false);

                hasEvents = events.Any();

                if (hasEvents)
                {
                    await ReconcileAsync(events)
                        .ConfigureAwait(false);

                    OnEventSequenceAdvanced(lastSequence!.Value);

                    previous = lastSequence;
                }
            }
            while (hasEvents);

            return previous;
        }

        protected abstract Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
            ulong? previous,
            ulong? target = default);

        protected abstract Task ReconcileAsync(IEnumerable<DomainEvent> events);

        protected void OnEventsReconciled(IEnumerable<DomainEvent> events)
        {
            EventsReconciled?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventsReconciling(IEnumerable<DomainEvent> events)
        {
            EventsReconciling?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventSequenceAdvanced(ulong current)
        {
            EventSequenceAdvanced?.Invoke(this, new EventSequenceAdvancedEventArgs(current));
        }
    }
}