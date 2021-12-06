namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Reference;

    public class WhenEqualityIsChecked
    {
        [Fact]
        public void GivenAnInstanceAndANullReferenceThenBothAreNotConsideredEqual()
        {
            Reference first = Create<SerializableAggregateRoot>(Guid.NewGuid());
            Reference<SerializableAggregateRoot>? second = default;

            Assert.False(first == second);
            Assert.False(first.Equals(second));
            Assert.False(second == first);
        }

        [Fact]
        public void GivenANullInstanceAndAnInstanceThenBothAreNotConsideredEqual()
        {
            Reference<SerializableAggregateRoot>? first = default;
            Reference second = Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
            Assert.False(second.Equals(first));
            Assert.False(second == first);
        }

        [Fact]
        public void GivenANullInstancesThenBothAreNotConsideredEqual()
        {
            Reference<SerializableAggregateRoot>? first = default;
            Reference<SerializableAggregateRoot>? second = default;

            Assert.True(first == second);
            Assert.True(second == first);
        }

        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            Reference first = Create<SerializableAggregateRoot>(aggregate.Id);
            Reference second = Create(aggregate);

            Assert.True(first == second);
            Assert.True(first.Equals(second));
            Assert.True(second == first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
        {
            Reference first = Create<SerializableAggregateRoot>(Guid.NewGuid());
            Reference second = Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
            Assert.False(first.Equals(second));
            Assert.False(second == first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            Reference first = Create<SerializableAggregateRoot>(aggregateId);
            Reference second = Create<SerializableAggregateRoot>(aggregateId);

            Assert.True(first == second);
            Assert.True(first.Equals(second));
            Assert.True(second == first);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            Reference first = Create<SerializableAggregateRoot>(aggregateId);
            Reference second = Create<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.False(first == second);
            Assert.False(first.Equals(second));
            Assert.False(second == first);
        }
    }
}