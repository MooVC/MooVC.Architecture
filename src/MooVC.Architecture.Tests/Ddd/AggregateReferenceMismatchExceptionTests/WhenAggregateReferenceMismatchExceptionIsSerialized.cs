namespace MooVC.Architecture.Ddd.AggregateReferenceMismatchExceptionTests
{
    using System;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateReferenceMismatchExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var reference = Guid.NewGuid().ToReference<AggregateRoot>();
            var original = new AggregateReferenceMismatchException<EventCentricAggregateRoot>(reference);
            AggregateReferenceMismatchException<EventCentricAggregateRoot> deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Reference, deserialized.Reference);
        }
    }
}