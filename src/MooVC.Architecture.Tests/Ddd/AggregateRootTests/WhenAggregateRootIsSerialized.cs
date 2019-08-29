namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateRootIsSerialized
    {
        [Theory]
        [InlineData(1)]
        [InlineData(18446744073709551615)]
        public void GivenAnInstanceThenAllPropertiesAreSerialized(ulong expectedVersion)
        {
            var expectedId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(expectedId, version: expectedVersion);
            SerializableAggregateRoot clone = aggregate.Clone();

            Assert.Equal(aggregate, clone);
            Assert.NotSame(aggregate, clone);

            Assert.Equal(expectedId, clone.Id);
            Assert.Equal(expectedVersion, clone.Version);
            Assert.Equal(aggregate.GetHashCode(), clone.GetHashCode());
        }
    }
}