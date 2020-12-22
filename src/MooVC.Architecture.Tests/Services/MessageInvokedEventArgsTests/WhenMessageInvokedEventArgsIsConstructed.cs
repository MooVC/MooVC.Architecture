namespace MooVC.Architecture.Services.MessageInvokedEventArgsTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenMessageInvokedEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsCreated()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokedEventArgs(message);

            Assert.Equal(message, @event.Message);
            Assert.Same(message, @event.Message);
        }

        [Fact]
        public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
        {
            SerializableMessage message = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new MessageInvokedEventArgs(message));

            Assert.Equal(nameof(message), exception.ParamName);
        }
    }
}