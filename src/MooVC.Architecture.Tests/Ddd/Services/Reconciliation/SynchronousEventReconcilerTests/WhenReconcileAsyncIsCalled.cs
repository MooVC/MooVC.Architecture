namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousEventReconcilerTests
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class WhenReconcileAsyncIsCalled
    {
        [Fact]
        public async Task GivenEventsThenTheReconcilerIsInvokedAsync()
        {
            bool wasInvoked = false;

            var reconciler = new TestableSynchronousEventReconciler(events: _ => wasInvoked = true);

            _ = await reconciler.ReconcileAsync();

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenEventsWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousEventReconciler();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => reconciler.ReconcileAsync());
        }
    }
}