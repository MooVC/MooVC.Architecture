namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconcilerTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenReconcileAsyncIsCalled
    {
        [Fact]
        public async Task GivenAggregatesThenTheAggregatesArePassedToTheReconcilerAsync()
        {
            bool wasInvoked = false;

            SerializableEventCentricAggregateRoot[]? expected = new[]
            {
                new SerializableEventCentricAggregateRoot(),
            };

            var reconciler = new TestableSynchronousAggregateReconciler(aggregates: actual =>
            {
                wasInvoked = true;

                Assert.Equal(expected, actual);
            });

            await reconciler.ReconcileAsync(expected);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenEventsThenTheEventsArePassedToTheReconcilerAsync()
        {
            bool wasInvoked = false;

            SerializableCreatedDomainEvent[]? expected = new[]
            {
                new SerializableCreatedDomainEvent(
                    new SerializableMessage(),
                    new SerializableEventCentricAggregateRoot()),
            };

            var reconciler = new TestableSynchronousAggregateReconciler(events: actual =>
            {
                wasInvoked = true;

                Assert.Equal(expected, actual);
            });

            await reconciler.ReconcileAsync(expected);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenAggregatesWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var store = new TestableSynchronousAggregateReconciler();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => store.ReconcileAsync(default(EventCentricAggregateRoot[])!));
        }

        [Fact]
        public async Task GivenEventsWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var store = new TestableSynchronousAggregateReconciler();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => store.ReconcileAsync(default(DomainEvent[])!));
        }
    }
}