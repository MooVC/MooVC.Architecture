namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousReconciliationOrchestratorTests;

using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

public sealed class WhenReconcileAsyncIsCalled
{
    [Fact]
    public async Task GivenASequenceThenTheReconcilerIsInvokedWithTheSequenceAsync()
    {
        bool wasInvoked = false;
        var expected = new Mock<IEventSequence>();

        var reconciler = new TestableSynchronousReconciliationOrchestrator(reconciler: actual =>
        {
            wasInvoked = true;

            Assert.Equal(expected.Object, actual);
        });

        await reconciler.ReconcileAsync(target: expected.Object);

        Assert.True(wasInvoked);
    }

    [Fact]
    public async Task GivenASequenceWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var reconciler = new TestableSynchronousReconciliationOrchestrator();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => reconciler.ReconcileAsync());
    }
}