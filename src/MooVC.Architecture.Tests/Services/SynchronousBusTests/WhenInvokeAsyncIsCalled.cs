namespace MooVC.Architecture.Services.SynchronousBusTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenInvokeAsyncIsCalled
    {
        [Fact]
        public async Task GivenAQueryThenAResultIsReturnedAsync()
        {
            bool wasInvoked = false;
            var expected = new SerializableMessage();

            var handler = new TestableSynchronousBus(invoke: actual =>
            {
                wasInvoked = true;

                Assert.Equal(expected, actual);
            });

            await handler.InvokeAsync(expected);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenAQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var handler = new TestableSynchronousBus();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => handler.InvokeAsync(new SerializableMessage()));
        }
    }
}