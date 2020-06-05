namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
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
            SequenceStore = new Mock<IStore<IEventSequence, ulong>>();
            SnapshotStore = new Mock<IStore<ISnapshot, ulong>>();
        }

        protected Mock<IAggregateReconciler> AggregateReconciler { get; }

        protected Mock<IStore<EventCentricAggregateRoot, Guid>> AggregateStore { get; }

        protected Mock<IEventReconciler> EventReconciler { get; }

        protected Mock<IStore<IEventSequence, ulong>> SequenceStore { get; }

        protected Mock<IStore<ISnapshot, ulong>> SnapshotStore { get; }
    }
}