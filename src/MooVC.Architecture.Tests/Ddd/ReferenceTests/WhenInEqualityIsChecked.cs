namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Reference;

    public class WhenInEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            Reference first = Create<SerializableAggregateRoot>(aggregate.Id);
            Reference second = Create(aggregate);

            Assert.False(first != second);
            Assert.False(second != first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            Reference first = Create<SerializableAggregateRoot>(aggregateId);
            Reference second = Create<SerializableAggregateRoot>(aggregateId);

            Assert.False(first != second);
            Assert.False(second != first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
        {
            Reference first = Create<SerializableAggregateRoot>(Guid.NewGuid());
            Reference second = Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.True(first != second);
            Assert.True(second != first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            Reference first = Create<SerializableAggregateRoot>(aggregateId);
            Reference second = Create<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.True(first != second);
            Assert.True(second != first);
        }
    }
}