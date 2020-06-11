namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Persistence;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DefaultReconciliationOrchestrator<TEventSequence, TSnapshot>
        : IReconciliationOrchestrator
        where TEventSequence : class, IEventSequence
        where TSnapshot : class, ISnapshot
    {
        private readonly IAggregateReconciler aggregateReconciler;
        private readonly IStore<EventCentricAggregateRoot, Guid> aggregateStore;
        private readonly IEventReconciler eventReconciler;
        private readonly IStore<TEventSequence, ulong> sequenceStore;
        private readonly IStore<TSnapshot, ulong> snapshotStore;
        private readonly Func<ulong, TEventSequence> sequenceFactory;

        public DefaultReconciliationOrchestrator(
            IAggregateReconciler aggregateReconciler,
            IStore<EventCentricAggregateRoot, Guid> aggregateStore,
            IEventReconciler eventReconciler,
            Func<ulong, TEventSequence> sequenceFactory,
            IStore<TEventSequence, ulong> sequenceStore,
            IStore<TSnapshot, ulong> snapshotStore)
        {
            ArgumentNotNull(aggregateReconciler, nameof(aggregateReconciler), DefaultReconciliationOrchestratorAggregateReconcilerRequired);
            ArgumentNotNull(aggregateStore, nameof(aggregateStore), DefaultReconciliationOrchestratorAggregateStoreRequired);
            ArgumentNotNull(eventReconciler, nameof(eventReconciler), DefaultReconciliationOrchestratorEventReconcilerRequired);
            ArgumentNotNull(sequenceFactory, nameof(sequenceFactory), DefaultReconciliationOrchestratorSequenceFactoryRequired);
            ArgumentNotNull(sequenceStore, nameof(sequenceStore), DefaultReconciliationOrchestratorSequenceStoreRequired);
            ArgumentNotNull(snapshotStore, nameof(snapshotStore), DefaultReconciliationOrchestratorSnapshotStoreRequired);

            this.aggregateReconciler = aggregateReconciler;
            this.aggregateStore = aggregateStore;
            this.eventReconciler = eventReconciler;
            this.sequenceFactory = sequenceFactory;
            this.sequenceStore = sequenceStore;
            this.snapshotStore = snapshotStore;

            this.aggregateReconciler.AggregateConflictDetected += AggregateReconciler_AggregateConflictDetected;
            this.eventReconciler.EventSequenceAdvanced += EventReconciler_EventSequenceAdvanced;
        }

        public event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        public void Reconcile(IEventSequence target = default)
        {
            IEventSequence previous = GetPreviousSequence();

            if (previous is null)
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
            EventCentricAggregateRoot aggregate = aggregateStore.Get(e.Aggregate.Id);

            sender.Reconcile(aggregate);
        }

        private void EventReconciler_EventSequenceAdvanced(IEventReconciler sender, EventSequenceAdvancedEventArgs e)
        {
            UpdateSequence(e.Sequence);
        }
    }
}