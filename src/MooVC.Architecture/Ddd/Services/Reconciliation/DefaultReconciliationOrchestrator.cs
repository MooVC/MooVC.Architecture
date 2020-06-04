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
        private readonly Func<ulong, IEventSequence, IEventSequence> sequenceFactory;

        public DefaultReconciliationOrchestrator(
            IAggregateReconciler aggregateReconciler,
            IStore<EventCentricAggregateRoot, Guid> aggregateStore,
            IEventReconciler eventReconciler,
            IStore<IEventSequence, ulong> sequenceStore,
            IStore<ISnapshot, ulong> snapshotStore,
            Func<ulong, IEventSequence, IEventSequence> sequenceFactory = default)
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
            this.sequenceFactory = sequenceFactory ?? DefaultSequenceFactory;

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

            ulong? current = ReconcileEvents(previous, target);

            return UpdateSequence(current, previous);
        }

        private static IEventSequence DefaultSequenceFactory(ulong current, IEventSequence previous)
        {
            return new EventSequence(current);
        }

        private IEventSequence GetPreviousSequence()
        {
            return sequenceStore.Get().LastOrDefault();
        }

        private ulong? ReconcileEvents(IEventSequence previous, IEventSequence target)
        {
            return eventReconciler.Reconcile(previous: previous?.Sequence, target: target?.Sequence);
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

        private IEventSequence UpdateSequence(ulong? current, IEventSequence previous)
        {
            if (current.HasValue)
            {
                IEventSequence sequence = sequenceFactory(current.Value, previous);

                _ = sequenceStore.Create(sequence);

                return sequence;
            }

            return previous;
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