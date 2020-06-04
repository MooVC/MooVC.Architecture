namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Persistence;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DefaultReconciliationOrchestrator
        : IReconciliationOrchestrator
    {
        private readonly IAggregateReconciler aggregateReconciler;
        private readonly IStore<EventCentricAggregateRoot, Guid> aggregateStore;
        private readonly IEventReconciler eventReconciler;
        private readonly IStore<IEventSequence, ulong> sequenceStore;
        private readonly IStore<ISnapshot, ulong> snapshotStore;

        public DefaultReconciliationOrchestrator(
            IAggregateReconciler aggregateReconciler,
            IStore<EventCentricAggregateRoot, Guid> aggregateStore,
            IEventReconciler eventReconciler,
            IStore<IEventSequence, ulong> sequenceStore,
            IStore<ISnapshot, ulong> snapshotStore)
        {
            ArgumentNotNull(aggregateReconciler, nameof(aggregateReconciler), DefaultReconciliationOrchestratorAggregateReconcilerRequired);
            ArgumentNotNull(aggregateStore, nameof(aggregateStore), DefaultReconciliationOrchestratorAggregateStoreRequired);
            ArgumentNotNull(eventReconciler, nameof(eventReconciler), DefaultReconciliationOrchestratorEventReconcilerRequired);
            ArgumentNotNull(sequenceStore, nameof(sequenceStore), DefaultReconciliationOrchestratorSequenceStoreRequired);
            ArgumentNotNull(snapshotStore, nameof(snapshotStore), DefaultReconciliationOrchestratorSnapshotStoreRequired);

            this.aggregateReconciler = aggregateReconciler;
            this.aggregateStore = aggregateStore;
            this.eventReconciler = eventReconciler;
            this.sequenceStore = sequenceStore;
            this.snapshotStore = snapshotStore;

            aggregateReconciler.AggregateConflictDetected += AggregateReconciler_AggregateConflictDetected;
        }

        public event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        public IEventSequence Reconcile(IEventSequence target = default)
        {
            IEventSequence previous = GetPreviousSequence();

            if (previous is null)
            {
                previous = RestoreLatestSnapshot();
            }

            IEventSequence current = ReconcileEvents(previous, target);

            UpdateSequence(current);

            return current;
        }

        private IEventSequence GetPreviousSequence()
        {
            return sequenceStore.Get().LastOrDefault();
        }

        private IEventSequence ReconcileEvents(IEventSequence previous, IEventSequence target)
        {
            return eventReconciler.Reconcile(previous: previous, target: target);
        }

        private IEventSequence RestoreLatestSnapshot()
        {
            ISnapshot latest = snapshotStore.Get().LastOrDefault();

            if (latest is { })
            {
                SnapshotRestorationCommencing?.Invoke(this, EventArgs.Empty);

                aggregateReconciler.Reconcile(latest.Aggregates.ToArray());

                SnapshotRestorationCompleted?.Invoke(this, new SnapshotRestorationCompletedEventArgs(latest.Sequence));

                return latest.Sequence;
            }

            return default;
        }

        private void UpdateSequence(IEventSequence current)
        {
            _ = sequenceStore.Create(current);
        }

        private void AggregateReconciler_AggregateConflictDetected(
            IAggregateReconciler sender,
            AggregateConflictDetectedEventArgs e)
        {
            EventCentricAggregateRoot aggregate = aggregateStore.Get(e.Aggregate.Id);

            sender.Reconcile(aggregate);
        }
    }
}