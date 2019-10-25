namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using Xunit;

    public class WhenReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeAndVersionThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();
            ulong version = 1;

            var first = new VersionedReference<AggregateRoot>(aggregateId, version: version);
            var second = new VersionedReference<AggregateRoot>(aggregateId, version: version);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdTypeAndDifferentVersionThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();
            
            var first = new VersionedReference<AggregateRoot>(aggregateId, version: 1);
            var second = new VersionedReference<AggregateRoot>(aggregateId, version: 2);

            Assert.False(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndVersionButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<AggregateRoot>(aggregateId);
            var second = new Reference<EventCentricAggregateRoot>(aggregateId);

            Assert.False(first == second);
        }
    }
}