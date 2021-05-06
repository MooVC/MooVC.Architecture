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
            var original = new SerializableMessage();
            SerializableMessage deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(original.CausationId, deserialized.CausationId);
            Assert.Equal(original.CorrelationId, deserialized.CorrelationId);
            Assert.Equal(original.Id, deserialized.Id);
            Assert.Equal(original.TimeStamp, deserialized.TimeStamp);

            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }

        [Fact]
        public void GivenAnInstanceBasedOnAnInstanceThenAllPropertiesAreSerialized()
        {
            var expected = new SerializableMessage();
            var original = new SerializableMessage(context: expected);
            SerializableMessage deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(original.CausationId, deserialized.CausationId);
            Assert.Equal(original.CorrelationId, deserialized.CorrelationId);
            Assert.Equal(original.Id, deserialized.Id);
            Assert.Equal(original.TimeStamp, deserialized.TimeStamp);

            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}