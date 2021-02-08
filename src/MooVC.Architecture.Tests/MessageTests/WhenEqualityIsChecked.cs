namespace MooVC.Architecture.MessageTests
{
    using System;
    using MooVC.Architecture.EntityTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenEqualityIsChecked
    {
        [Fact]
        public void GivenTwoMessagesWithTheSameIdAndTypeThenAPositiveResponseIsReturned()
        {
            var first = new SerializableMessage();
            SerializableMessage second = first.Clone();

            Assert.Equal(first, second);
            Assert.True(first == second);
            Assert.True(first.Equals(second));
        }

        [Fact]
        public void GivenTwoMessagesWithDifferentIdsAndTheSameTypeThenANegativeResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableMessage();

            Assert.NotEqual(first, second);
            Assert.False(first == second);
            Assert.False(first.Equals(second));
        }

        [Fact]
        public void GivenTwoEntitiesWithTheSameIdsButDifferentTypesThenANegativeResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableEntity<Guid>(first.Id);

            Assert.False(first == second);
            Assert.False(first.Equals(second));
        }
    }
}