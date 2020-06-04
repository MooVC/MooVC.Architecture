namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Persistence;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DefaultEventReconciler
        : EventReconciler
    {
        public const ushort DefaultNumberToRead = 200;
        public const ushort MinimumNumberToRead = 1;

        private readonly IEventStore<SequencedEvents, ulong> eventStore;
        private readonly IAggregateReconciler reconciler;
        private readonly IStore<EventSequence, ulong> sequenceStore;
        private readonly ushort numberToRead;

        public DefaultEventReconciler(
            IEventStore<SequencedEvents, ulong> eventStore,
            IAggregateReconciler reconciler,
            IStore<EventSequence, ulong> sequenceStore,
            ushort numberToRead = DefaultNumberToRead)
        {
            ArgumentNotNull(eventStore, nameof(eventStore), DefaultEventReconcilerEventStoreRequired);
            ArgumentNotNull(reconciler, nameof(reconciler), DefaultEventReconcilerReconcilerRequired);
            ArgumentNotNull(sequenceStore, nameof(sequenceStore), DefaultEventReconcilerSequenceStoreRequired);

            this.eventStore = eventStore;
            this.reconciler = reconciler;
            this.sequenceStore = sequenceStore;
            this.numberToRead = Math.Max(MinimumNumberToRead, numberToRead);
        }

        protected override IEnumerable<DomainEvent> GetEvents(IEventSequence previous, out ulong lastSequence)
        {
            IEnumerable<SequencedEvents> sequences = eventStore.Read(previous.Sequence, numberToRead: numberToRead);

            lastSequence = sequences
                .Select(sequence => sequence.Sequence)
                .DefaultIfEmpty()
                .Max();

            return sequences
                .SelectMany(sequence => sequence.Events)
                .ToArray();
        }

        protected override IEventSequence GetPreviousSequence()
        {
            return sequenceStore.Get().LastOrDefault() ?? new EventSequence(default);
        }

        protected override void Reconcile(IEnumerable<DomainEvent> events)
        {
            foreach (IGrouping<Type, DomainEvent> type in events
                .GroupBy(@event => @event.Aggregate.Type))
            {
                foreach (IGrouping<Guid, DomainEvent> aggregate in type.GroupBy(type => type.Aggregate.Id))
                {
                    PerformReconciliation(aggregate);
                }
            }
        }

        protected override IEventSequence UpdateSequence(IEventSequence previous, ulong lastSequence)
        {
            var current = new EventSequence(lastSequence);

            _ = sequenceStore.Create(current);

            return current;
        }

        private void PerformReconciliation(IEnumerable<DomainEvent> events)
        {
            OnEventsReconciling(events);

            reconciler.Reconcile(events);

            OnEventsReconciled(events);
        }
    }
}