namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Persistence;
    using Moq;

    public abstract class DefaultReconciliationOrchestratorTests
    {
        protected DefaultReconciliationOrchestratorTests()
        {
            AggregateReconciler = new Mock<IAggregateReconciler>();
            AggregateStore = new Mock<IStore<EventCentricAggregateRoot, Guid>>();
            EventReconciler = new Mock<IEventReconciler>();
            SequenceFactory = sequence => new EventSequence(sequence);
            SequenceStore = new Mock<IStore<EventSequence, ulong>>();
            SnapshotStore = new Mock<IStore<Snapshot, ulong>>();
        }

        protected Mock<IAggregateReconciler> AggregateReconciler { get; }

        protected Mock<IStore<EventCentricAggregateRoot, Guid>> AggregateStore { get; }

        protected Mock<IEventReconciler> EventReconciler { get; }

        protected Func<ulong, EventSequence> SequenceFactory { get; }

        protected Mock<IStore<EventSequence, ulong>> SequenceStore { get; }

        protected Mock<IStore<Snapshot, ulong>> SnapshotStore { get; }
    }
}