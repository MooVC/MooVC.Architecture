namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using Xunit;

    public sealed class WhenAggregateRootIsConstructed
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(18446744073709551615)]
        public void GivenAnIdAndAVersionThenTheIdAndVersionArePropagated(ulong expectedVersion)
        {
            var expectedId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(expectedId, version: expectedVersion);

            Assert.Equal(expectedId, aggregate.Id);
            Assert.Equal(expectedVersion, aggregate.Version);
        }

        [Fact]
        public void GivenAnIdAndNoVersionThenTheIdIsPropagatedAndTheVersionDefaulted()
        {
            var expectedId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(expectedId);

            Assert.Equal(expectedId, aggregate.Id);
            Assert.Equal(AggregateRoot.DefaultVersion, aggregate.Version);
        }
    }
}