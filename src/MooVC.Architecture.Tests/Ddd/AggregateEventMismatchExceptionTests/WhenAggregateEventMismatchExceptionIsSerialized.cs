namespace MooVC.Architecture.Ddd.AggregateEventMismatchExceptionTests
{
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateEventMismatchExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var subject = new SerializableAggregateRoot();
            Reference aggregate = subject.ToReference();
            Reference eventAggregate = subject.ToReference();
            var original = new AggregateEventMismatchException(aggregate, eventAggregate);
            AggregateEventMismatchException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.EventAggregate, deserialized.EventAggregate);
        }
    }
}