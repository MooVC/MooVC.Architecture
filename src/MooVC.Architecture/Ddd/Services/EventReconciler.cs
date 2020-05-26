namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class EventReconciler
        : IEventReconciler
    {
        public event EventsReconciledEventHandler EventsReconciled;

        public event EventsReconcilingEventHandler EventsReconciling;

        public event EventSequenceAdvancedEventHandler EventSequenceAdvanced;

        public ulong Reconcile()
        {
            IEventSequence current = GetCurrentSequence();
            bool hasEvents;

            do
            {
                IEnumerable<DomainEvent> events = GetEvents(current, out ulong lastSequence);

                hasEvents = events.Any();

                if (hasEvents)
                {
                    Reconcile(events);

                    current = UpdateSequence(current, lastSequence);

                    OnEventSequenceAdvanced(current);
                }
            }
            while (hasEvents);

            return current.Sequence;
        }

        protected abstract IEventSequence GetCurrentSequence();

        protected abstract IEnumerable<DomainEvent> GetEvents(IEventSequence current, out ulong lastSequence);

        protected abstract void Reconcile(IEnumerable<DomainEvent> events);

        protected abstract IEventSequence UpdateSequence(IEventSequence current, ulong lastSequence);

        protected void OnEventsReconciled(IEnumerable<DomainEvent> events)
        {
            EventsReconciled?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventsReconciling(IEnumerable<DomainEvent> events)
        {
            EventsReconciling?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventSequenceAdvanced(IEventSequence sequence)
        {
            EventSequenceAdvanced?.Invoke(this, new EventSequenceAdvancedEventArgs(sequence));
        }
    }
}