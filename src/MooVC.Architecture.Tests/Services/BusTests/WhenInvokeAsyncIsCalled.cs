namespace MooVC.Architecture.Services.BusTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenInvokeAsyncIsCalled
    {
        [Fact]
        public async Task GivenAMessageThenMessageInvokingIsRaisedBeforeTheInvokeIsPerformedAsync()
        {
            var bus = new TestableBus(true);
            var message = new SerializableMessage();
            bool wasInvoked = false;

            bus.Invoking += (_, _) => Task.FromResult(wasInvoked = true);

            _ = await Assert.ThrowsAsync<InvalidOperationException>(
                () => bus.InvokeAsync(message));

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenAMessageThenMessageInvokingIsRaisedAfterTheInvokeIsPerformedAsync()
        {
            var bus = new TestableBus(false);
            var message = new SerializableMessage();
            bool wasInvoked = false;

            bus.Invoked += (_, _) => Task.FromResult(wasInvoked = true);

            await bus.InvokeAsync(message);

            Assert.True(wasInvoked);
        }

        [Fact]
        public async Task GivenANullMessageThenAnArgumentNullExceptionIsThrownAsync()
        {
            var bus = new TestableBus(false);
            Message? message = default;

            ArgumentNullException? exception = await Assert.ThrowsAsync<ArgumentNullException>(
                () => bus.InvokeAsync(message!));

            Assert.Equal(nameof(message), exception.ParamName);
        }

        [Fact]
        public async Task GivenANullMessageThenNoEventsAreRaisedAsync()
        {
            var bus = new TestableBus(false);
            Message? message = default;
            bool wasInvoked = false;

            bus.Invoking += (_, _) => Task.FromResult(wasInvoked = true);
            bus.Invoked += (_, _) => Task.FromResult(wasInvoked = true);

            _ = await Assert.ThrowsAsync<ArgumentNullException>(
                () => bus.InvokeAsync(message!));

            Assert.False(wasInvoked);
        }
    }
}