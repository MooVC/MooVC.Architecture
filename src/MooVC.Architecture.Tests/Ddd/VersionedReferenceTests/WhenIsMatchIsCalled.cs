namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.ReferenceTests;
    using Moq;
    using Xunit;

    public sealed class WhenIsMatchIsCalled
    {
        [Theory]
        [InlineData(1ul)]
        [InlineData(18446744073709551615)]
        public void GivenAMatchingReferenceThenTrueIsReturned(ulong version)
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId, version);
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregateId, version: version);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(null));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new VersionedReference<AggregateRoot>(aggregateId);

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
        public void GivenAReferenceWithTheSameIdAndTypeButDifferentVersionThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, 5ul);
            var reference = new VersionedReference<AggregateRoot>(aggregateId, 2ul);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var referenceId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new VersionedReference<AggregateRoot>(referenceId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndVersionButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new VersionedReference<EventCentricAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }
    }
}
