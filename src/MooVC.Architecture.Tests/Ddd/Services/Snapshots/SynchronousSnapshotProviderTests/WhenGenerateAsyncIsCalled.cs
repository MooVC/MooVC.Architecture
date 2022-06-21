namespace MooVC.Architecture.Ddd.Services.Snapshots.SynchronousSnapshotProviderTests;

using System;
using System.Threading.Tasks;
using Moq;
using Xunit;

public sealed class WhenGenerateAsyncIsCalled
{
    [Fact]
    public async Task GivenATargetThenTheGeneratorIsInvokedWithTheTargetAsync()
    {
        bool wasInvoked = false;
        ulong expectedTarget = 1;
        var expectedSnapshot = new Mock<ISnapshot>();

        var reconciler = new TestableSynchronousSnapshotProvider(generator: actualTarget =>
        {
            wasInvoked = true;

            Assert.Equal(expectedTarget, actualTarget);

            return expectedSnapshot.Object;
        });

        ISnapshot? actualSnapshot = await reconciler.GenerateAsync(target: expectedTarget);

        Assert.True(wasInvoked);
        Assert.Equal(expectedSnapshot.Object, actualSnapshot);
    }

    [Fact]
    public async Task GivenEventsWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var provider = new TestableSynchronousSnapshotProvider();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => provider.GenerateAsync());
    }
}