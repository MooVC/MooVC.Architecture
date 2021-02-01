namespace MooVC.Architecture.MessageTests
{
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenMessageIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var message = new SerializableMessage();
            SerializableMessage clone = message.Clone();

            Assert.Equal(message, clone);
            Assert.NotSame(message, clone);

            Assert.Equal(message.CausationId, clone.CausationId);
            Assert.Equal(message.CorrelationId, clone.CorrelationId);
            Assert.Equal(message.Id, clone.Id);
            Assert.Equal(message.TimeStamp, clone.TimeStamp);

            Assert.Equal(message.GetHashCode(), clone.GetHashCode());
        }

        [Fact]
        public void GivenAnInstanceBasedOnAnInstanceThenAllPropertiesAreSerialized()
        {
            var expected = new SerializableMessage();
            var message = new SerializableMessage(expected);
            SerializableMessage clone = message.Clone();

            Assert.Equal(message, clone);
            Assert.NotSame(message, clone);

            Assert.Equal(message.CausationId, clone.CausationId);
            Assert.Equal(message.CorrelationId, clone.CorrelationId);
            Assert.Equal(message.Id, clone.Id);
            Assert.Equal(message.TimeStamp, clone.TimeStamp);

            Assert.Equal(message.GetHashCode(), clone.GetHashCode());
        }
    }
}