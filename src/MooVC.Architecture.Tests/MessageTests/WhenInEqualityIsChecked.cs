namespace MooVC.Architecture.MessageTests
{
    using System;
    using MooVC.Architecture.EntityTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenInEqualityIsChecked
    {
        [Fact]
        public void GivenTwoMessagesWithTheSameIdAndTypeThenANegativeResponseIsReturned()
        {
            var first = new SerializableMessage();
            SerializableMessage second = first.Clone();

            Assert.False(first != second);
        }

        [Fact]
        public void GivenTwoMessagesWithDifferentIdsAndTheSameTypeThenAPositiveResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableMessage();

            Assert.True(first != second);
        }

        [Fact]
        public void GivenTwoEntitiesWithTheSameIdsButDifferentTypesThenAPositiveResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableEntity<Guid>(first.Id);

            Assert.True(first != second);
        }
    }
}