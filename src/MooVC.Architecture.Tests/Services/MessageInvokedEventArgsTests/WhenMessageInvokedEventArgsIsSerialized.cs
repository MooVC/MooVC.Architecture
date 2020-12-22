namespace MooVC.Architecture.Services.MessageInvokedEventArgsTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenMessageInvokedEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokedEventArgs(message);

            MessageInvokedEventArgs deserialized = @event.Clone();

            Assert.Equal(@event.Message, deserialized.Message);
            Assert.NotSame(@event.Message, deserialized.Message);
            Assert.NotSame(@event, deserialized);
        }
    }
}