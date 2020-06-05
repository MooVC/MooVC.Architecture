namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Linq;
    using Moq;
    using Xunit;

    public sealed class WhenReconcileIsCalled
        : DefaultReconciliationOrchestratorTests
    {
        private readonly DefaultReconciliationOrchestrator instance;

        public WhenReconcileIsCalled()
        {
            instance = new DefaultReconciliationOrchestrator(
                AggregateReconciler.Object,
                AggregateStore.Object,
                EventReconciler.Object,
                SequenceStore.Object,
                SnapshotStore.Object);
        }

        [Fact]
        public void GivenAPreviousSequenceThenSnapshotRecoveryIsNotTriggered()
        {
            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { new EventSequence(1) });

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;

            instance.SnapshotRestorationCommencing += (_, __) => wasSnapshotRestorationCommencingInvoked = true;
            instance.SnapshotRestorationCompleted += (_, __) => wasSnapshotRestorationCompletedInvoked = true;

            instance.Reconcile();

            Assert.False(wasSnapshotRestorationCommencingInvoked);
            Assert.False(wasSnapshotRestorationCompletedInvoked);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
            SnapshotStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Never);
        }

        [Fact]
        public void GivenAPreviousSequenceThenEventReconciliationIsTriggeredFromThatSequence()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { sequence });

            instance.Reconcile();

            EventReconciler.Verify(
                reconciler => reconciler.Reconcile(It.IsAny<ulong?>(), It.IsAny<ulong?>()),
                times: Times.Once);

            EventReconciler.Verify(
                reconciler => reconciler.Reconcile(
                    It.Is<ulong?>(previous => previous == sequence.Sequence), It.IsAny<ulong?>()),
                times: Times.Once);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
        }

        [Fact]
        public void GivenNoPreviousSequenceThenSnapshotRecoveryIsTriggered()
        {
            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new IEventSequence[0]);

            _ = SnapshotStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new ISnapshot[0]);

            instance.Reconcile();

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
            SnapshotStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
        }

        [Fact]
        public void GivenNoPreviousSequenceAndASnapshotThenTheSnapshotIsRestored()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);
            var snapshot = new Snapshot(new[] { aggregate }, sequence);

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new IEventSequence[0]);

            _ = SnapshotStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { snapshot });

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;

            instance.SnapshotRestorationCommencing += (_, __) => wasSnapshotRestorationCommencingInvoked = true;
            instance.SnapshotRestorationCompleted += (_, __) => wasSnapshotRestorationCompletedInvoked = true;

            instance.Reconcile();

            Assert.True(wasSnapshotRestorationCommencingInvoked);
            Assert.True(wasSnapshotRestorationCompletedInvoked);

            AggregateReconciler.Verify(reconciler => reconciler.Reconcile(It.IsAny<EventCentricAggregateRoot[]>()), times: Times.Once);

            AggregateReconciler.Verify(
                reconciler => reconciler.Reconcile(It.Is<EventCentricAggregateRoot[]>(
                    aggregates => aggregates.SequenceEqual(snapshot.Aggregates))),
                times: Times.Once);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
            SnapshotStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
        }
    }
}