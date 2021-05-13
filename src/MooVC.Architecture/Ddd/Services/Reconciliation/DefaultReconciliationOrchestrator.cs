namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Persistence;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class DefaultReconciliationOrchestrator<TEventSequence>
        : ReconciliationOrchestrator
        where TEventSequence : class, IEventSequence
    {
        private readonly IAggregateReconciler aggregateReconciler;
        private readonly Func<Reference, EventCentricAggregateRoot> aggregateSource;
        private readonly IEventReconciler eventReconciler;
        private readonly Func<ulong, TEventSequence> sequenceFactory;
        private readonly IStore<TEventSequence, ulong> sequenceStore;
        private readonly Func<ISnapshot> snapshotSource;

        public DefaultReconciliationOrchestrator(
            IAggregateReconciler aggregateReconciler,
            Func<Reference, EventCentricAggregateRoot> aggregateSource,
            IEventReconciler eventReconciler,
            Func<ulong, TEventSequence> sequenceFactory,
            IStore<TEventSequence, ulong> sequenceStore,
            Func<ISnapshot> snapshotSource)
        {
            ArgumentNotNull(aggregateReconciler, nameof(aggregateReconciler), DefaultReconciliationOrchestratorAggregateReconcilerRequired);
            ArgumentNotNull(aggregateSource, nameof(aggregateSource), DefaultReconciliationOrchestratorAggregateSourceRequired);
            ArgumentNotNull(eventReconciler, nameof(eventReconciler), DefaultReconciliationOrchestratorEventReconcilerRequired);
            ArgumentNotNull(sequenceFactory, nameof(sequenceFactory), DefaultReconciliationOrchestratorSequenceFactoryRequired);
            ArgumentNotNull(sequenceStore, nameof(sequenceStore), DefaultReconciliationOrchestratorSequenceStoreRequired);
            ArgumentNotNull(snapshotSource, nameof(snapshotSource), DefaultReconciliationOrchestratorSnapshotSourceRequired);

            this.aggregateReconciler = aggregateReconciler;
            this.aggregateSource = aggregateSource;
            this.eventReconciler = eventReconciler;
            this.sequenceFactory = sequenceFactory;
            this.sequenceStore = sequenceStore;
            this.snapshotSource = snapshotSource;

            this.aggregateReconciler.AggregateConflictDetected += AggregateReconciler_AggregateConflictDetected;
            this.eventReconciler.EventSequenceAdvanced += EventReconciler_EventSequenceAdvanced;
        }

        public override async Task ReconcileAsync(IEventSequence? target = default)
        {
            IEventSequence? previous = await GetPreviousSequenceAsync()
                .ConfigureAwait(false);

            if (previous is null || previous.Sequence == 0)
            {
                previous = await RestoreLatestSnapshotAsync()
                    .ConfigureAwait(false);
            }

            ReconcileEvents(previous, target);
        }

        private async Task<IEventSequence?> GetPreviousSequenceAsync()
        {
            IEnumerable<TEventSequence>? last = await sequenceStore
                .GetAsync()
                .ConfigureAwait(false);

            return last.LastOrDefault();
        }

        private void ReconcileEvents(IEventSequence? previous, IEventSequence? target)
        {
            _ = eventReconciler.ReconcileAsync(previous: previous?.Sequence, target: target?.Sequence);
        }

        private async Task<IEventSequence?> RestoreLatestSnapshotAsync()
        {
            ISnapshot latest = snapshotSource();

            if (latest is { })
            {
                OnSnapshotRestorationCommencing();

                await aggregateReconciler
                    .ReconcileAsync(latest.Aggregates.ToArray())
                    .ConfigureAwait(false);

                await UpdateSequenceAsync(latest.Sequence.Sequence)
                    .ConfigureAwait(false);

                OnSnapshotRestorationCompleted(latest.Sequence);

                return latest.Sequence;
            }

            return default;
        }

        private async Task UpdateSequenceAsync(ulong current)
        {
            TEventSequence sequence = sequenceFactory(current);

            if (sequence is { })
            {
                _ = await sequenceStore
                    .CreateAsync(sequence)
                    .ConfigureAwait(false);
            }
        }

        private async Task AggregateReconciler_AggregateConflictDetected(
            IAggregateReconciler sender,
            AggregateConflictDetectedEventArgs e)
        {
            EventCentricAggregateRoot aggregate = aggregateSource(e.Aggregate);

            await sender
                .ReconcileAsync(aggregate)
                .ConfigureAwait(false);
        }

        private async Task EventReconciler_EventSequenceAdvanced(
            IEventReconciler sender,
            EventSequenceAdvancedEventArgs e)
        {
            await UpdateSequenceAsync(e.Sequence)
                .ConfigureAwait(false);
        }
    }
}