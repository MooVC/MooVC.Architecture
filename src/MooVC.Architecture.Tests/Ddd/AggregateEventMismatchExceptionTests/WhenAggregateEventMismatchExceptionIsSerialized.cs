namespace MooVC.Architecture.Ddd.AggregateEventMismatchExceptionTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateEventMismatchExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var subject = new SerializableAggregateRoot();
            VersionedReference aggregate = subject.ToVersionedReference();
            VersionedReference eventAggregate = subject.ToVersionedReference();
            var original = new AggregateEventMismatchException(aggregate, eventAggregate);
            AggregateEventMismatchException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.EventAggregate, deserialized.EventAggregate);
        }
    }
}