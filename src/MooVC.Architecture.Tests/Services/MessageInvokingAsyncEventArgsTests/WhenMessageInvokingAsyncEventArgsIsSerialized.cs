namespace MooVC.Architecture.Services.MessageInvokingAsyncEventArgsTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenMessageInvokingAsyncEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokingAsyncEventArgs(message);

            MessageInvokingAsyncEventArgs deserialized = @event.Clone();

            Assert.Equal(@event.Message, deserialized.Message);
            Assert.NotSame(@event.Message, deserialized.Message);
            Assert.NotSame(@event, deserialized);
        }
    }
}