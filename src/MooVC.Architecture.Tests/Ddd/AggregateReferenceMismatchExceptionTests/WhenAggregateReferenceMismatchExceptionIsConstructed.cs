namespace MooVC.Architecture.Ddd.AggregateReferenceMismatchExceptionTests
{
    using System;
    using Xunit;

    public sealed class WhenAggregateReferenceMismatchExceptionIsConstructed
    {
        [Fact]
        public void GivenAReferenceThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var reference = Guid.NewGuid().ToReference<SerializableAggregateRoot>();
            var original = new AggregateReferenceMismatchException<SerializableEventCentricAggregateRoot>(reference);

            Assert.Equal(reference, original.Reference);
        }

        [Fact]
        public void GivenANullReferenceThenAnArgumentNullExceptionIsThrown()
        {
            Reference? reference = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateReferenceMismatchException<EventCentricAggregateRoot>(reference!));

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}