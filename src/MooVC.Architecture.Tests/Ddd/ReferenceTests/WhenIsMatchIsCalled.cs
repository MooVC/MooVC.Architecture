namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
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

            Assert.False(condition: reference.IsMatch(default!));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new DerivedAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceForADerivedTypeThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<DerivedAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var referenceId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(referenceId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }
    }
}