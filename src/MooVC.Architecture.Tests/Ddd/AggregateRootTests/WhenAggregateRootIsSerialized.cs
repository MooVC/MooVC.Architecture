namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateRootIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            SerializableAggregateRoot clone = aggregate.Clone();

            Assert.Equal(aggregate, clone);
            Assert.NotSame(aggregate, clone);

            Assert.Equal(aggregate.Id, clone.Id);
            Assert.Equal(aggregate.Version, clone.Version);
            Assert.Equal(aggregate.GetHashCode(), clone.GetHashCode());
        }
    }
}