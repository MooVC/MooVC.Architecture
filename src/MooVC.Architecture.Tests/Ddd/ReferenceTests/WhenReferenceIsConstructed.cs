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
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);

            var reference = new Reference<AggregateRoot>(aggregate.Object);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
        }

        [Fact]
        public void GivenAnAggregateIdThenTheIdAndTypeArePropagated()
        {
            var aggregateId = Guid.NewGuid();

            var reference = new Reference<AggregateRoot>(aggregateId);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
        }
    }
}