namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.ReferenceTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public class WhenVersionedReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferenceAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<SerializableAggregateRoot>(aggregate);
            var second = new Reference<SerializableAggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeAndVersionThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<SerializableAggregateRoot>(aggregate);
            var second = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdTypeAndDifferentVersionThenBothAreNotConsideredEqual()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var first = new VersionedReference<SerializableEventCentricAggregateRoot>(aggregate);
            var context = new SerializableMessage();

            aggregate.MarkChangesAsCommitted();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            var second = new VersionedReference<SerializableEventCentricAggregateRoot>(aggregate);

            Assert.False(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndVersionButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<DerivedAggregateRoot>(aggregate.Id, aggregate.Version);
            var second = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.False(first == second);
        }
    }
}