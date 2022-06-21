namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public sealed class WhenGetAllAsyncIsCalled
{
    [Fact]
    public async Task GivenAReferenceThenTheReferenceIsPropagatedAsync()
    {
        bool wasInvoked = false;
        SerializableEventCentricAggregateRoot[] expected = new[] { new SerializableEventCentricAggregateRoot() };

        var proxy = new TestableSynchronousAggregateReconciliationProxy(getAll: () =>
        {
            wasInvoked = true;

            return expected;
        });

        IEnumerable<EventCentricAggregateRoot>? actual = await proxy.GetAllAsync();

        Assert.True(wasInvoked);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GivenAnExceptionThenTheExceptionIsThrownAsync()
    {
        var reconciler = new TestableSynchronousAggregateReconciliationProxy();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => reconciler.GetAllAsync());
    }
}