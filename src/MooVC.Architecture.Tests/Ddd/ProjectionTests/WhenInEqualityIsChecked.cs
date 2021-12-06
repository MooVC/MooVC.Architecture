namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using System;
    using Xunit;

    public sealed class WhenInEqualityIsChecked
    {
        [Fact]
        public void GivenAMatchingProjectionThenANegativeResponseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var first = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var second = new SerializableProjection<SerializableAggregateRoot>(aggregate);

            Assert.False(first != second);
            Assert.False(second != first);
        }

        [Fact]
        public void GivenAMatchingReferenceThenANegativeResponseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var first = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var second = aggregate.ToReference();

            Assert.False(first != second);
            Assert.False(second != first);
        }

        [Fact]
        public void GivenAMismatchingProjectionThenAPositiveResponseIsReturned()
        {
            var first = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            var second = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            Assert.True(first != second);
            Assert.True(second != first);
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAPositiveResponseIsReturned()
        {
            var first = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            var second = Reference.Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.True(first != second);
            Assert.True(second != first);
        }
    }
}