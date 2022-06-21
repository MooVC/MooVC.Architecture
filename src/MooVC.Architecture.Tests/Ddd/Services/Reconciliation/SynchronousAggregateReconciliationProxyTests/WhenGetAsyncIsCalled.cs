namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests;

using System;
using System.Threading.Tasks;
using Xunit;

public sealed class WhenGetAsyncIsCalled
{
    [Fact]
    public async Task GivenAReferenceThenTheReferenceIsPropagatedAsync()
    {
        bool wasInvoked = false;
        var expectedAggregate = new SerializableEventCentricAggregateRoot();
        var expectedReference = expectedAggregate.ToReference();

        var proxy = new TestableSynchronousAggregateReconciliationProxy(get: actualReference =>
        {
            wasInvoked = true;

            Assert.Equal(expectedReference, actualReference);

            return expectedAggregate;
        });

        EventCentricAggregateRoot? actualAggregate = await proxy.GetAsync(expectedReference);

        Assert.True(wasInvoked);
        Assert.Equal(expectedAggregate, actualAggregate);
    }

    [Fact]
    public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
    {
        var reconciler = new TestableSynchronousAggregateReconciliationProxy();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => reconciler.GetAsync(default!));
    }
}