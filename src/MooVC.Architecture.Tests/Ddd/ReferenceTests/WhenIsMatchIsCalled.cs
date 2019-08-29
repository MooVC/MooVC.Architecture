namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
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
            var reference = new Reference<SerializableAggregateRoot>(aggregateId, version);

            Assert.True(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var reference = new Reference<SerializableAggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(null));
        }

        [Fact]
        public void GivenAReferenceForABaseTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceForADerivedTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<DerivedAggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndTypeButDifferentVersionThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, 5ul);
            var reference = new Reference<AggregateRoot>(aggregateId, 2ul);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithADifferentIdButTheSameTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var referenceId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<AggregateRoot>(referenceId, AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }
        
        [Fact]
        public void GivenAReferenceWithTheSameIdAndVersionButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<EventCentricAggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }
    }
}
