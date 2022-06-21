namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests;

using System;
using System.Threading.Tasks;
using Xunit;

public sealed class WhenSaveAsyncIsCalled
{
    [Fact]
    public async Task GivenAnAggregateThenTheAggregateIsPropagatedAsync()
    {
        bool wasInvoked = false;

        var expected = new SerializableEventCentricAggregateRoot();

        var proxy = new TestableSynchronousAggregateReconciliationProxy(save: actual =>
        {
            wasInvoked = true;

            Assert.Equal(expected, actual);
        });

        await proxy.SaveAsync(expected);

        Assert.True(wasInvoked);
    }

    [Fact]
    public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
    {
        var reconciler = new TestableSynchronousAggregateReconciliationProxy();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => reconciler.SaveAsync(default!));
    }
}