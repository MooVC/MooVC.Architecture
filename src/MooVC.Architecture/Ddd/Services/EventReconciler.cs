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

        public IEventSequence Reconcile(IEventSequence previous = default, IEventSequence target = default)
        {
            previous ??= GetPreviousSequence();

            bool hasEvents;

            do
            {
                IEnumerable<DomainEvent> events = GetEvents(previous, out ulong lastSequence, target: target);

                hasEvents = events.Any();

                if (hasEvents)
                {
                    Reconcile(events);

                    previous = UpdateSequence(previous, lastSequence);

                    OnEventSequenceAdvanced(previous);
                }
            }
            while (hasEvents);

            return previous;
        }

        protected abstract IEnumerable<DomainEvent> GetEvents(
            IEventSequence previous,
            out ulong lastSequence,
            IEventSequence target = default);

        protected abstract IEventSequence GetPreviousSequence();

        protected abstract void Reconcile(IEnumerable<DomainEvent> events);

        protected abstract IEventSequence UpdateSequence(IEventSequence previous, ulong lastSequence);

        protected void OnEventsReconciled(IEnumerable<DomainEvent> events)
        {
            EventsReconciled?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventsReconciling(IEnumerable<DomainEvent> events)
        {
            EventsReconciling?.Invoke(this, new EventReconciliationEventArgs(events));
        }

        protected void OnEventSequenceAdvanced(IEventSequence current)
        {
            EventSequenceAdvanced?.Invoke(this, new EventSequenceAdvancedEventArgs(current));
        }
    }
}