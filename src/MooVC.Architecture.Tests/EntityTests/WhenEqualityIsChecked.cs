namespace MooVC.Architecture.EntityTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenEqualityIsChecked
    {
        [Fact]
        public void GivenTwoEntitiesWithDifferentIdsAndTheSameTypeThenANegativeResponseIsReturned()
        {
            var first = new SerializableEntity<int>(1);
            var second = new SerializableEntity<int>(2);

            Assert.NotEqual(first, second);
            Assert.False(first == second);
            Assert.False(first.Equals(second));
            Assert.False(second == first);
        }

        [Fact]
        public void GivenTwoEntitiesWithTheSameIdAndTypeThenAPositiveResponseIsReturned()
        {
            var first = new SerializableEntity<int>(1);
            SerializableEntity<int> second = first.Clone();

            Assert.Equal(first, second);
            Assert.True(first == second);
            Assert.True(first.Equals(second));
            Assert.True(second == first);
        }

        [Fact]
        public void GivenTwoEntitiesWithTheSameIdsButDifferentTypesThenANegativeResponseIsReturned()
        {
            var first = new SerializableMessage();
            var second = new SerializableEntity<Guid>(first.Id);

            Assert.False(first == second);
            Assert.False(first.Equals(second));
            Assert.False(second == first);
        }
    }
}