namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class EventReconciler
        : IEventReconciler
    {
        public event EventsReconciledEventHandler? EventsReconciled;

        public event EventsReconcilingEventHandler? EventsReconciling;

        public event EventSequenceAdvancedEventHandler? EventSequenceAdvanced;

        public ulong? Reconcile(ulong? previous = default, ulong? target = default)
        {
            bool hasEvents;

            do
            {
                IEnumerable<DomainEvent> events = GetEvents(previous, out ulong? lastSequence, target: target);

                hasEvents = events.Any();

                if (hasEvents)
                {
                    Reconcile(events);

                    OnEventSequenceAdvanced(lastSequence!.Value);

                    previous = lastSequence;
                }
            }
            while (hasEvents);

            return previous;
        }

        protected abstract IEnumerable<DomainEvent> GetEvents(
            ulong? previous,
            out ulong? lastSequence,
            ulong? target = default);

        protected abstract void Reconcile(IEnumerable<DomainEvent> events);

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