namespace MooVC.Architecture.Services.MessageInvokingEventArgsTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenMessageInvokingEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsCreated()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokingEventArgs(message);

            Assert.Equal(message, @event.Message);
            Assert.Same(message, @event.Message);
        }

        [Fact]
        public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
        {
            SerializableMessage? message = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new MessageInvokingEventArgs(message!));

            Assert.Equal(nameof(message), exception.ParamName);
        }
    }
}