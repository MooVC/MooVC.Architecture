namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenOverwriteAsyncIsCalled
    {
        [Fact]
        public async Task GivenAnAggregateThenTheAggregateIsPropagatedAsync()
        {
            bool wasInvoked = false;

            var expected = new SerializableEventCentricAggregateRoot();

            var proxy = new TestableSynchronousAggregateReconciliationProxy(overwrite: actual =>
            {
                wasInvoked = true;

                Assert.Equal(expected, actual);
            });

            await proxy.OverwriteAsync(expected);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => reconciler.OverwriteAsync(default!));
        }
    }
}