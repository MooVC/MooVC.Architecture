namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Linq;
    using Moq;
    using Xunit;

    public sealed class WhenReconcileIsCalled
        : DefaultReconciliationOrchestratorTests
    {
        [Fact]
        public void GivenAPreviousSequenceThenSnapshotRecoveryIsNotTriggered()
        {
            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { new EventSequence(1) });

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;
            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return default!;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            instance.SnapshotRestorationCommencing += (_, __) => wasSnapshotRestorationCommencingInvoked = true;
            instance.SnapshotRestorationCompleted += (_, __) => wasSnapshotRestorationCompletedInvoked = true;

            instance.Reconcile();

            Assert.False(wasSnapshotRestorationCommencingInvoked);
            Assert.False(wasSnapshotRestorationCompletedInvoked);
            Assert.False(wasTriggered);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
        }

        [Fact]
        public void GivenAPreviousSequenceThenEventReconciliationIsTriggeredFromThatSequence()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { sequence });

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler();

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
                .Returns(new EventSequence[0]);

            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return default!;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            instance.Reconcile();

            Assert.True(wasTriggered);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
        }

        [Fact]
        public void GivenNoPreviousSequenceAndASnapshotThenTheSnapshotIsRestored()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);
            var snapshot = new Snapshot(new[] { aggregate }, sequence);

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new EventSequence[0]);

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;
            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return snapshot;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            instance.SnapshotRestorationCommencing += (_, __) => wasSnapshotRestorationCommencingInvoked = true;
            instance.SnapshotRestorationCompleted += (_, __) => wasSnapshotRestorationCompletedInvoked = true;

            instance.Reconcile();

            Assert.True(wasSnapshotRestorationCommencingInvoked);
            Assert.True(wasSnapshotRestorationCompletedInvoked);
            Assert.True(wasTriggered);

            AggregateReconciler.Verify(reconciler => reconciler.Reconcile(It.IsAny<EventCentricAggregateRoot[]>()), times: Times.Once);

            AggregateReconciler.Verify(
                reconciler => reconciler.Reconcile(It.Is<EventCentricAggregateRoot[]>(
                    aggregates => aggregates.SequenceEqual(snapshot.Aggregates))),
                times: Times.Once);

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
            SequenceStore.Verify(store => store.Create(It.IsAny<EventSequence>()), times: Times.Once);
            SequenceStore.Verify(store => store.Create(It.Is<EventSequence>(updated => updated.Sequence == sequence.Sequence)), times: Times.Once);
        }

        private DefaultReconciliationOrchestrator<EventSequence> CreateReconciler(Func<Snapshot>? snapshotSource = default)
        {
            snapshotSource ??= () => default!;

            return new DefaultReconciliationOrchestrator<EventSequence>(
                AggregateReconciler.Object,
                reference => default!,
                EventReconciler.Object,
                SequenceFactory,
                SequenceStore.Object,
                snapshotSource);
        }
    }
}