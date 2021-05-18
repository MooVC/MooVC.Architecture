namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Snapshots;
    using MooVC.Linq;
    using Moq;
    using Xunit;

    public sealed class WhenReconcileAsyncIsCalled
        : DefaultReconciliationOrchestratorTests
    {
        [Fact]
        public async Task GivenAPreviousSequenceThenSnapshotRecoveryIsNotTriggeredAsync()
        {
            _ = SequenceStore
                .Setup(store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()))
                .ReturnsAsync(new[] { new EventSequence(1) });

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;
            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return default!;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            instance.SnapshotRestorationCommencing += (_, _) => Task.FromResult(wasSnapshotRestorationCommencingInvoked = true);
            instance.SnapshotRestorationCompleted += (_, _) => Task.FromResult(wasSnapshotRestorationCompletedInvoked = true);

            await instance.ReconcileAsync();

            Assert.False(wasSnapshotRestorationCommencingInvoked);
            Assert.False(wasSnapshotRestorationCompletedInvoked);
            Assert.False(wasTriggered);

            SequenceStore.Verify(
                store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()),
                times: Times.Once);
        }

        [Fact]
        public async Task GivenAPreviousSequenceThenEventReconciliationIsTriggeredFromThatSequenceAsync()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);

            _ = SequenceStore
                .Setup(store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()))
                .ReturnsAsync(new[] { sequence });

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler();

            await instance.ReconcileAsync();

            EventReconciler.Verify(
                reconciler => reconciler.ReconcileAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<ulong?>(),
                    It.IsAny<ulong?>()),
                times: Times.Once);

            EventReconciler.Verify(
                reconciler => reconciler.ReconcileAsync(
                    It.IsAny<CancellationToken?>(),
                    It.Is<ulong?>(previous => previous == sequence.Sequence),
                    It.IsAny<ulong?>()),
                times: Times.Once);

            SequenceStore.Verify(
                store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()),
                times: Times.Once);
        }

        [Fact]
        public async Task GivenNoPreviousSequenceThenSnapshotRecoveryIsTriggeredAsync()
        {
            _ = SequenceStore
                .Setup(store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()))
                .ReturnsAsync(Enumerable.Empty<EventSequence>());

            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return default!;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            await instance.ReconcileAsync();

            Assert.True(wasTriggered);

            SequenceStore.Verify(
                store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()),
                times: Times.Once);
        }

        [Fact]
        public async Task GivenNoPreviousSequenceAndASnapshotThenTheSnapshotIsRestoredAsync()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var sequence = new EventSequence(10);
            var snapshot = new Snapshot(new[] { aggregate }, sequence);

            _ = SequenceStore
                .Setup(store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()))
                .ReturnsAsync(Enumerable.Empty<EventSequence>());

            bool wasSnapshotRestorationCommencingInvoked = false;
            bool wasSnapshotRestorationCompletedInvoked = false;
            bool wasTriggered = false;

            Snapshot SnapshotSource()
            {
                wasTriggered = true;

                return snapshot;
            }

            DefaultReconciliationOrchestrator<EventSequence> instance = CreateReconciler(snapshotSource: SnapshotSource);

            instance.SnapshotRestorationCommencing += (_, _) => Task.FromResult(wasSnapshotRestorationCommencingInvoked = true);
            instance.SnapshotRestorationCompleted += (_, _) => Task.FromResult(wasSnapshotRestorationCompletedInvoked = true);

            await instance.ReconcileAsync();

            Assert.True(wasSnapshotRestorationCommencingInvoked);
            Assert.True(wasSnapshotRestorationCompletedInvoked);
            Assert.True(wasTriggered);

            AggregateReconciler.Verify(
                reconciler => reconciler.ReconcileAsync(
                    It.IsAny<IEnumerable<EventCentricAggregateRoot>>(),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            AggregateReconciler.Verify(
                reconciler => reconciler.ReconcileAsync(
                    It.Is<IEnumerable<EventCentricAggregateRoot>>(
                        aggregates => aggregates.SequenceEqual(snapshot.Aggregates)),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            SequenceStore.Verify(
                store => store.GetAsync(
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<Paging>()),
                times: Times.Once);

            SequenceStore.Verify(
                store => store.CreateAsync(
                    It.IsAny<EventSequence>(),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            SequenceStore.Verify(
                store => store.CreateAsync(
                    It.Is<EventSequence>(updated => updated.Sequence == sequence.Sequence),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);
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