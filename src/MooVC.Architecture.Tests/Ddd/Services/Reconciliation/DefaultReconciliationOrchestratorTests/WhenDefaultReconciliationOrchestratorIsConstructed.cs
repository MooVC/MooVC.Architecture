namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using Xunit;

    public sealed class WhenDefaultReconciliationOrchestratorIsConstructed
        : DefaultReconciliationOrchestratorTests
    {
        [Fact]
        public void GivenAnAggregateReconcilerAnAgggregateStoreAnEventReconcilerASequenceStoreAndANullSnapshotStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                AggregateStore.Object,
                EventReconciler.Object,
                SequenceStore.Object,
                null));
        }

        [Fact]
        public void GivenAnAggregateReconcilerAnAgggregateStoreAnEventReconcilerASnapshotStoreAndANullSequenceStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                AggregateStore.Object,
                EventReconciler.Object,
                null,
                SnapshotStore.Object));
        }

        [Fact]
        public void GivenAnAggregateReconcilerAnAgggregateStoreASequenceStoreASnapshotStoreAndANullEventReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                AggregateStore.Object,
                null,
                SequenceStore.Object,
                SnapshotStore.Object));
        }

        [Fact]
        public void GivenAnAggregateReconcilerAnEventReconcilerASequenceStoreASnapshotStoreAndANullAgggregateStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                null,
                EventReconciler.Object,
                SequenceStore.Object,
                SnapshotStore.Object));
        }

        [Fact]
        public void GivenAnAgggregateStoreAnEventReconcilerASequenceStoreASnapshotStoreAndANullAggregateReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator(
                null,
                AggregateStore.Object,
                EventReconciler.Object,
                SequenceStore.Object,
                SnapshotStore.Object));
        }

        [Fact]
        public void GivenAnAggregateReconcilerAnAgggregateStoreAnEventReconcilerASequenceStoreASnapshotStoreThenAnInstanceIsCreated()
        {
            _ = new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                AggregateStore.Object,
                EventReconciler.Object,
                SequenceStore.Object,
                SnapshotStore.Object);
        }
    }
}