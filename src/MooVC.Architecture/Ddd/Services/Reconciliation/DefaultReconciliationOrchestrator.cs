namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Persistence;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DefaultReconciliationOrchestrator<TEventSequence>
        : IReconciliationOrchestrator
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

        public event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        public void Reconcile(IEventSequence target = default)
        {
            IEventSequence previous = GetPreviousSequence();

            if (previous is null || previous.Sequence == 0)
            {
                previous = RestoreLatestSnapshot();
            }

            ReconcileEvents(previous, target);
        }

        private IEventSequence GetPreviousSequence()
        {
            return sequenceStore.Get().LastOrDefault();
        }

        private void ReconcileEvents(IEventSequence previous, IEventSequence target)
        {
            _ = eventReconciler.Reconcile(previous: previous?.Sequence, target: target?.Sequence);
        }

        private IEventSequence RestoreLatestSnapshot()
        {
            ISnapshot latest = snapshotSource();

            if (latest is { })
            {
                SnapshotRestorationCommencing?.Invoke(this, EventArgs.Empty);

                aggregateReconciler.Reconcile(latest.Aggregates.ToArray());

                SnapshotRestorationCompleted?.Invoke(this, new SnapshotRestorationCompletedEventArgs(latest.Sequence));

                return latest.Sequence;
            }

            return default;
        }

        private void UpdateSequence(ulong current)
        {
            TEventSequence sequence = sequenceFactory(current);

            if (sequence is { })
            {
                _ = sequenceStore.Create(sequence);
            }
        }

        private void AggregateReconciler_AggregateConflictDetected(
            IAggregateReconciler sender,
            AggregateConflictDetectedEventArgs e)
        {
            EventCentricAggregateRoot aggregate = aggregateSource(e.Aggregate);

            sender.Reconcile(aggregate);
        }

        private void EventReconciler_EventSequenceAdvanced(IEventReconciler sender, EventSequenceAdvancedEventArgs e)
        {
            UpdateSequence(e.Sequence);
        }
    }
}