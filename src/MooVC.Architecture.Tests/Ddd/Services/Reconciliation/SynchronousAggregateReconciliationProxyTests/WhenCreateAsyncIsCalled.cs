namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenCreateAsyncIsCalled
    {
        [Fact]
        public async Task GivenAReferenceThenTheReferenceIsPropagatedAsync()
        {
            bool wasInvoked = false;
            var expectedAggregate = new SerializableEventCentricAggregateRoot();
            var expectedReference = expectedAggregate.ToReference();

            var proxy = new TestableSynchronousAggregateReconciliationProxy(create: actualReference =>
            {
                wasInvoked = true;

                Assert.Equal(expectedReference, actualReference);

                return expectedAggregate;
            });

            EventCentricAggregateRoot? actualAggregate = await proxy.CreateAsync(expectedReference);

            Assert.True(wasInvoked);
            Assert.Equal(expectedAggregate, actualAggregate);
        }

        [Fact]
        public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => reconciler.CreateAsync(default!));
        }
    }
}