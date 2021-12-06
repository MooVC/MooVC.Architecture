namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Reference;

    public sealed class WhenIsMatchIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            Reference reference = Create<SerializableAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            Reference reference = Create<SerializableAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(default!));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new DerivedAggregateRoot(aggregateId);
            Reference reference = Create<SerializableAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceForADerivedTypeThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            Reference reference = Create<DerivedAggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var referenceId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            Reference reference = Create<SerializableAggregateRoot>(referenceId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            Reference reference = Create<SerializableEventCentricAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate));
        }
    }
}