namespace MooVC.Architecture.Services.MessageInvokingEventArgsTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenMessageInvokingEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var message = new SerializableMessage();
            var @event = new MessageInvokingEventArgs(message);

            MessageInvokingAsyncEventArgs deserialized = @event.Clone();

            Assert.Equal(@event.Message, deserialized.Message);
            Assert.NotSame(@event.Message, deserialized.Message);
            Assert.NotSame(@event, deserialized);
        }
    }
}