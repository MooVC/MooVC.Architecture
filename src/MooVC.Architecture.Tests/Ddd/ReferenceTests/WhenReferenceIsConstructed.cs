namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Moq;
    using Xunit;

    public sealed class WhenReferenceIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenTheIdAndTypeArePropagated()
        {
            var expectedId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(expectedId);

            var reference = new Reference<SerializableAggregateRoot>(aggregate);

            Assert.Equal(expectedId, reference.Id);
            Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
        }

        [Fact]
        public void GivenAnAggregateIdThenTheIdAndTypeArePropagated()
        {
            var expectedId = Guid.NewGuid();

            var reference = new Reference<SerializableAggregateRoot>(expectedId);

            Assert.Equal(expectedId, reference.Id);
            Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
        }

        [Fact]
        public void GivenAnEmptyIdThenAnArgumentExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentException>(() => new Reference<AggregateRoot>(Guid.Empty));
        }
    }
}