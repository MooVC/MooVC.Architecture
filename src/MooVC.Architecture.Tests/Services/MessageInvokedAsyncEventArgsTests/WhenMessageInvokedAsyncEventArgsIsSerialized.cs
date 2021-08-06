namespace MooVC.Architecture.Services.MessageInvokedAsyncEventArgsTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenMessageInvokedAsyncEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokedAsyncEventArgs(message);

            MessageInvokedAsyncEventArgs deserialized = @event.Clone();

            Assert.Equal(@event.Message, deserialized.Message);
            Assert.NotSame(@event.Message, deserialized.Message);
            Assert.NotSame(@event, deserialized);
        }
    }
}