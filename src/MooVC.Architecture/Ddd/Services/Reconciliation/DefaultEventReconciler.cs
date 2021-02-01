namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Persistence;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class DefaultEventReconciler<TSequencedEvents>
        : EventReconciler
        where TSequencedEvents : class, ISequencedEvents
    {
        public const ushort DefaultNumberToRead = 200;
        public const ushort MinimumNumberToRead = 1;

        private readonly IEventStore<TSequencedEvents, ulong> eventStore;
        private readonly IAggregateReconciler reconciler;
        private readonly ushort numberToRead;

        public DefaultEventReconciler(
            IEventStore<TSequencedEvents, ulong> eventStore,
            IAggregateReconciler reconciler,
            ushort numberToRead = DefaultNumberToRead)
        {
            ArgumentNotNull(eventStore, nameof(eventStore), DefaultEventReconcilerEventStoreRequired);
            ArgumentNotNull(reconciler, nameof(reconciler), DefaultEventReconcilerReconcilerRequired);

            this.eventStore = eventStore;
            this.reconciler = reconciler;
            this.numberToRead = Math.Max(MinimumNumberToRead, numberToRead);
        }

        protected override IEnumerable<DomainEvent> GetEvents(
            ulong? previous,
            out ulong? lastSequence,
            ulong? target = default)
        {
            if (ShouldReadEvents(previous, target, out ushort numberToRead, out ulong start))
            {
                IEnumerable<TSequencedEvents> sequences = eventStore.Read(
                    start,
                    numberToRead: numberToRead);

                lastSequence = sequences
                    .Select(sequence => sequence.Sequence)
                    .DefaultIfEmpty()
                    .Max();

                return sequences
                    .SelectMany(sequence => sequence.Events)
                    .ToArray();
            }

            lastSequence = previous;

            return Enumerable.Empty<DomainEvent>();
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

        private bool ShouldReadEvents(ulong? previous, ulong? target, out ushort numberToRead, out ulong start)
        {
            numberToRead = this.numberToRead;
            start = previous.GetValueOrDefault();

            if (target.HasValue)
            {
                if (target.Value <= start)
                {
                    return false;
                }

                ulong difference = target.Value - start;

                numberToRead = (ushort)Math.Min(numberToRead, difference);
            }

            return true;
        }

        private void PerformReconciliation(IEnumerable<DomainEvent> events)
        {
            OnEventsReconciling(events);

            reconciler.Reconcile(events.ToArray());

            OnEventsReconciled(events);
        }
    }
}