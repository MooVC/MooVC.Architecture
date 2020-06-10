namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventSequenceTests
{
    using System;
    using Xunit;

    public sealed class WhenEventSequenceIsConstructed
    {
        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void GivenASequenceThenAnInstanceIsCreated(ulong sequence)
        {
            var instance = new EventSequence(sequence);

            Assert.Equal(sequence, instance.Sequence);
        }

        [Fact]
        public void GivenASequenceAndALastUpdatedThenAnInstanceIsCreated()
        {
            DateTime timeStamp = DateTime.UtcNow;
            var instance = new EventSequence(2, timeStamp: timeStamp);

            Assert.Equal(timeStamp, instance.TimeStamp);
        }
    }
}