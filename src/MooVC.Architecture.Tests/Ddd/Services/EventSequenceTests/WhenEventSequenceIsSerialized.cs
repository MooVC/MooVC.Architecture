namespace MooVC.Architecture.Ddd.Services.EventSequenceTests
{
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenEventSequenceIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var sequence = new EventSequence(3);
            EventSequence deserialized = sequence.Clone();

            Assert.Equal(sequence.Sequence, deserialized.Sequence);
            Assert.Equal(sequence.LastUpdated, deserialized.LastUpdated);
            Assert.NotSame(sequence, deserialized);
        }
    }
}