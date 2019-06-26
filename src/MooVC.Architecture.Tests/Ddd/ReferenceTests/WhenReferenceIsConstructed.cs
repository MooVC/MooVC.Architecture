namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Moq;
    using Xunit;

    public sealed class WhenReferenceIsConstructed
    {
        [Fact]
        public void GivenAnAggregateWhenEnforceVersionIsFalseThenTheIdAndTypeArePropagated()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            var reference = new Reference<AggregateRoot>(aggregate.Object);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.False(reference.Version.HasValue);
        }

        [Fact]
        public void GivenAnAggregateWhenEnforceVersionIsTrueThenTheIdTypeAndVersionArePropagated()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            var reference = new Reference<AggregateRoot>(aggregate.Object, enforceVersion: true);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.Equal(AggregateRoot.DefaultVersion, reference.Version);
        }

        [Fact]
        public void GivenAnAggregateIdThenTheIdAndTypeArePropagated()
        {
            var aggregateId = Guid.NewGuid();

            var reference = new Reference<AggregateRoot>(aggregateId);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.False(reference.Version.HasValue);
        }

        [Fact]
        public void GivenAnAggregateIdAndAVersionThenTheIdTypeAndVersionArePropagated()
        {
            var aggregateId = Guid.NewGuid();

            var reference = new Reference<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            Assert.Equal(aggregateId, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.Equal(AggregateRoot.DefaultVersion, reference.Version);
        }
    }
}