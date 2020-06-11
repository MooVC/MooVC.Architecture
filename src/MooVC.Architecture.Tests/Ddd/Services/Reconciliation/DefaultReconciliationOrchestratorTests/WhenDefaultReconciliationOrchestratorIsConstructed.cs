namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using Xunit;

    public sealed class WhenDefaultReconciliationOrchestratorIsConstructed
        : DefaultReconciliationOrchestratorTests
    {
        [Fact]
        public void GivenEverythingExceptASnapshotStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                reference => default,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                null));
        }

        [Fact]
        public void GivenEverythingExceptASequenceStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                reference => default,
                EventReconciler.Object,
                SequenceFactory,
                null,
                () => default));
        }

        [Fact]
        public void GivenEverythingExceptASequenceFactoryThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                reference => default,
                EventReconciler.Object,
                null,
                SequenceStore.Object,
                () => default));
        }

        [Fact]
        public void GivenEverythingExceptAnEventReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                reference => default,
                null,
                SequenceFactory,
                SequenceStore.Object,
                () => default));
        }

        [Fact]
        public void GivenEverythingExceptAnAgggregateStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                null,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default));
        }

        [Fact]
        public void GivenEverythingExceptAnAggregateReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                null,
                reference => default,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default));
        }

        [Fact]
        public void GivenEverythingThenAnInstanceIsCreated()
        {
            _ = new DefaultReconciliationOrchestrator<EventSequence, Snapshot>(
                AggregateReconciler.Object,
                reference => default,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default);
        }
    }
}