namespace MooVC.Architecture.MessageTests
{
    using System;
    using MooVC.Architecture.EntityTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenInEqualityIsChecked
    {
        [Fact]
        public void GivenTwoMessagesWithTheSameIdAndTypeThenANegativeResponseIsReturned()
        {
            var first = new SerializableMessage();
            SerializableMessage second = first.Clone();

            Assert.False(first != second);
            Assert.False(second != first);
        }

        [Fact]
        public void GivenTwoMessagesWithDifferentIdsAndTheSameTypeThenAPositiveResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableMessage();

            Assert.True(first != second);
            Assert.True(second != first);
        }

        [Fact]
        public void GivenTwoEntitiesWithTheSameIdsButDifferentTypesThenAPositiveResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableEntity<Guid>(first.Id);

            Assert.True(first != second);
            Assert.True(second != first);
        }
    }
}