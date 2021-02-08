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
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                default!));
        }

        [Fact]
        public void GivenEverythingExceptASequenceStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                EventReconciler.Object,
                SequenceFactory,
                default!,
                () => default!));
        }

        [Fact]
        public void GivenEverythingExceptASequenceFactoryThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                EventReconciler.Object,
                default!,
                SequenceStore.Object,
                () => default!));
        }

        [Fact]
        public void GivenEverythingExceptAnEventReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                default!,
                SequenceFactory,
                SequenceStore.Object,
                () => default!));
        }

        [Fact]
        public void GivenEverythingExceptAnAgggregateStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                default!,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default!));
        }

        [Fact]
        public void GivenEverythingExceptAnAggregateReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new DefaultReconciliationOrchestrator<EventSequence>(
                default!,
                reference => default!,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default!));
        }

        [Fact]
        public void GivenEverythingThenAnInstanceIsCreated()
        {
            _ = new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                () => default!);
        }
    }
}