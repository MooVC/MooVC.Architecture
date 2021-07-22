namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
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

        protected override async Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
            ulong? previous,
            CancellationToken? cancellationToken = default,
            ulong? target = default)
        {
            ulong? lastSequence = previous;
            IEnumerable<DomainEvent> events = Enumerable.Empty<DomainEvent>();

            if (ShouldReadEvents(previous, target, out ushort numberToRead, out ulong start))
            {
                IEnumerable<TSequencedEvents> sequences = await eventStore
                    .ReadAsync(
                        start,
                        cancellationToken: cancellationToken,
                        numberToRead: numberToRead)
                    .ConfigureAwait(false);

                lastSequence = sequences
                    .Select(sequence => sequence.Sequence)
                    .DefaultIfEmpty()
                    .Max();

                events = sequences
                    .SelectMany(sequence => sequence.Events)
                    .ToArray();
            }

            return (lastSequence, events);
        }

        protected override async Task ReconcileAsync(
            IEnumerable<DomainEvent> events,
            CancellationToken? cancellationToken = default)
        {
            foreach (IGrouping<Type, DomainEvent> type in events
                .GroupBy(@event => @event.Aggregate.Type))
            {
                foreach (IGrouping<Guid, DomainEvent> aggregate in type.GroupBy(type => type.Aggregate.Id))
                {
                    await PerformReconciliationAsync(aggregate, cancellationToken)
                        .ConfigureAwait(false);
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

        private async Task PerformReconciliationAsync(
            IEnumerable<DomainEvent> events,
            CancellationToken? cancellationToken)
        {
            await OnEventsReconcilingAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await reconciler
                .ReconcileAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await OnEventsReconciledAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}