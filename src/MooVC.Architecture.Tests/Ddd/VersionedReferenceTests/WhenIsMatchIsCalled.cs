namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.ReferenceTests;
    using Moq;
    using Xunit;

    public sealed class WhenIsMatchIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenTrueIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.True(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenFalseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            Assert.False(reference.IsMatch(null));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenFalseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            Assert.False(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceForADerivedTypeThenFalseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new Reference<DerivedAggregateRoot>(aggregate.Id);

            Assert.False(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndTypeButDifferentVersionThenFalseIsReturned()
        {
            var firstAggregate = new SerializableAggregateRoot();
            var secondAggregate = new SerializableAggregateRoot(firstAggregate.Id);
            var reference = new VersionedReference<AggregateRoot>(firstAggregate);

            Assert.False(reference.IsMatch(secondAggregate));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var firstAggregate = new SerializableAggregateRoot();
            var secondAggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(firstAggregate);

            Assert.False(reference.IsMatch(secondAggregate));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndVersionButADifferentTypeThenFalseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<EventCentricAggregateRoot>(aggregate.Id, aggregate.Version);

            Assert.False(reference.IsMatch(aggregate));
        }
    }
}
