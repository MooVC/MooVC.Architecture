namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Moq;
    using Xunit;

    public sealed class WhenIsMatchIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(null));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<AggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceForADerivedTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<DerivedAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var referenceId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);
            var reference = new Reference<AggregateRoot>(referenceId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);
            var reference = new Reference<EventCentricAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }
    }
}
