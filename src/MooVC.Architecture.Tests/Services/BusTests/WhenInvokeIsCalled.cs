namespace MooVC.Architecture.Services.BusTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenInvokeIsCalled
    {
        [Fact]
        public void GivenAMessageThenMessageInvokingIsRaisedBeforeTheInvokeIsPerformed()
        {
            var bus = new TestableBus(true);
            var message = new SerializableMessage();
            bool wasInvoked = false;

            bus.Invoking += (_, __) => wasInvoked = true;

            _ = Assert.Throws<InvalidOperationException>(() => bus.Invoke(message));

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenAMessageThenMessageInvokingIsRaisedAfterTheInvokeIsPerformed()
        {
            var bus = new TestableBus(false);
            var message = new SerializableMessage();
            bool wasInvoked = false;

            bus.Invoked += (_, __) => wasInvoked = true;

            bus.Invoke(message);

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenANullMessageThenAnArgumentNullExceptionIsThrown()
        {
            var bus = new TestableBus(false);
            Message? message = default;

            ArgumentNullException? exception = Assert.Throws<ArgumentNullException>(
                () => bus.Invoke(message!));

            Assert.Equal(nameof(message), exception.ParamName);
        }

        [Fact]
        public void GivenANullMessageThenNoEventsAreRaised()
        {
            var bus = new TestableBus(false);
            Message? message = default;
            bool wasInvoked = false;

            bus.Invoking += (_, __) => wasInvoked = true;
            bus.Invoked += (_, __) => wasInvoked = true;

            _ = Assert.Throws<ArgumentNullException>(
                () => bus.Invoke(message!));

            Assert.False(wasInvoked);
        }
    }
}