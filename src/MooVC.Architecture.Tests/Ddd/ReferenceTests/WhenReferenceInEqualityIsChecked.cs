namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public class WhenReferenceInEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new Reference<SerializableAggregateRoot>(aggregate);
            var second = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.False(first != second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<SerializableAggregateRoot>(aggregateId);
            var second = new Reference<SerializableAggregateRoot>(aggregateId);

            Assert.False(first != second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
        {
            var first = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            var second = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.True(first != second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<SerializableAggregateRoot>(aggregateId);
            var second = new Reference<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.True(first != second);
        }
    }
}