namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Moq;
    using Xunit;

    public sealed class WhenReferenceIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenTheIdAndTypeArePropagated()
        {
            var expectedId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(expectedId, AggregateRoot.DefaultVersion);

            var reference = new Reference<AggregateRoot>(aggregate.Object);

            Assert.Equal(expectedId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
        }

        [Fact]
        public void GivenAnAggregateIdThenTheIdAndTypeArePropagated()
        {
            var expectedId = Guid.NewGuid();

            var reference = new Reference<AggregateRoot>(expectedId);

            Assert.Equal(expectedId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
        }

        [Fact]
        public void GivenAnEmptyIdThenAnArgumentExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentException>(() => new Reference<AggregateRoot>(Guid.Empty));
        }
    }
}