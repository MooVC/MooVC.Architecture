namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public class WhenReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new Reference<SerializableAggregateRoot>(aggregate);
            var second = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<SerializableAggregateRoot>(aggregateId);
            var second = new Reference<SerializableAggregateRoot>(aggregateId);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
        {
            var first = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            var second = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<SerializableAggregateRoot>(aggregateId);
            var second = new Reference<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.False(first == second);
        }

        [Fact]
        public void GivenAnInstanceAndANullReferenceThenBothAreNotConsideredEqual()
        {
            var first = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            Reference<SerializableAggregateRoot>? second = default;

            Assert.False(first == second);
        }

        [Fact]
        public void GivenANullInstanceAndAnInstanceThenBothAreNotConsideredEqual()
        {
            Reference<SerializableAggregateRoot>? first = default;
            var second = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
        }
    }
}