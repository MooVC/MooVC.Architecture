namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class WhenIsMatchIsCalled
    {
        public static IEnumerable<object[]> GivenAMatchingReferenceThenTrueIsReturnedData => new[]
        {
            new object[] { null },
            new object[] { 5ul }
        };

        [Theory]
        [MemberData(nameof(GivenAMatchingReferenceThenTrueIsReturnedData))]
        public void GivenAMatchingReferenceThenTrueIsReturned(ulong? version)
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, version);
            var reference = new Reference<AggregateRoot>(aggregateId, version: version);

            Assert.True(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndTypeButNoVersionThenTrueIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, 5ul);
            var reference = new Reference<AggregateRoot>(aggregateId);

            Assert.True(condition: reference.IsMatch(aggregate.Object));
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
            var reference = new Reference<AggregateRoot>(referenceId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdButADifferentTypeAndAndNoVersionThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<EventCentricAggregateRoot>(aggregateId);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }

        [Fact]
        public void GivenAReferenceWithTheSameIdAndVersionButADifferentTypeThenFalseIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<EventCentricAggregateRoot>(aggregateId, version: AggregateRoot.DefaultVersion);

            Assert.False(condition: reference.IsMatch(aggregate.Object));
        }
    }
}
