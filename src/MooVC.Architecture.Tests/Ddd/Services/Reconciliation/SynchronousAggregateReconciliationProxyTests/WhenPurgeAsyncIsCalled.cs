namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenPurgeAsyncIsCalled
    {
        [Fact]
        public async Task GivenAReferenceThenTheReferenceIsPropagatedAsync()
        {
            bool wasInvoked = false;

            var expected = new SerializableEventCentricAggregateRoot()
                .ToReference();

            var proxy = new TestableSynchronousAggregateReconciliationProxy(purge: actual =>
            {
                wasInvoked = true;

                Assert.Equal(expected, actual);
            });

            await proxy.PurgeAsync(expected);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => reconciler.PurgeAsync(default!));
        }
    }
}