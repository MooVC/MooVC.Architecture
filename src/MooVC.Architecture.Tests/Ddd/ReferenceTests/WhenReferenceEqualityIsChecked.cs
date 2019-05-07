namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;

    public class WhenReferenceEqualityIsChecked
    {
        [Fact]
        public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
        {
            var aggregateId = Guid.NewGuid();

            var first = new Reference<AggregateRoot>(aggregateId);
            var second = new Reference<AggregateRoot>(aggregateId);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenTwoSeparateInstancesWithDifferentIdsAndSameTypeThenBothAreConsideredNotEqual()
        {
            var first = new Reference<AggregateRoot>(Guid.NewGuid());
            var second = new Reference<AggregateRoot>(Guid.NewGuid());

            Assert.True(first != second);
        }
    }
}