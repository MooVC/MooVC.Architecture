namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public class WhenVersionedReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<AggregateRoot>(aggregate);
            var second = new Reference<AggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeAndVersionThenBothAreConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<AggregateRoot>(aggregate);
            var second = new VersionedReference<AggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdTypeAndDifferentVersionThenBothAreNotConsideredEqual()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var first = new VersionedReference<AggregateRoot>(aggregate);
            var context = new SerializableMessage();

            aggregate.MarkChangesAsCommitted();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            var second = new VersionedReference<AggregateRoot>(aggregate);

            Assert.False(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndVersionButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregate = new SerializableAggregateRoot();

            var first = new VersionedReference<AggregateRoot>(aggregate);
            var second = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.False(first == second);
        }
    }
}