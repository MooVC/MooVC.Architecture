namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;

    public class WhenReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<AggregateRoot>(aggregateId);
            var second = new VersionedReference<AggregateRoot>(aggregateId);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<AggregateRoot>(aggregateId);
            var second = new Reference<AggregateRoot>(aggregateId);

            Assert.True(first == second);
        }
        
        [Fact]
        public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
        {
            var first = new Reference<AggregateRoot>(Guid.NewGuid());
            var second = new Reference<AggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();
            
            var first = new Reference<AggregateRoot>(aggregateId);
            var second = new Reference<EventCentricAggregateRoot>(aggregateId);

            Assert.False(first == second);
        }
    }
}