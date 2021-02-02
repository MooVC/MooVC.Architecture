namespace MooVC.Architecture.EntityTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenInEqualityIsChecked
    {
        [Fact]
        public void GivenTwoEntitiesWithTheSameIdAndTypeThenANegativeResponseIsReturned()
        {
            var first = new SerializableEntity<int>(1);
            SerializableEntity<int> second = first.Clone();

            Assert.False(first != second);
        }

        [Fact]
        public void GivenTwoEntitiesWithDifferentIdsAndTheSameTypeThenAPositiveResponseIsReturned()
        {
            var first = new SerializableEntity<int>(1);
            var second = new SerializableEntity<int>(2);

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