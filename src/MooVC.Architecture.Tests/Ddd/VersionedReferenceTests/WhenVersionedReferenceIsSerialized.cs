namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenVersionedReferenceIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var original = new VersionedReference<SerializableAggregateRoot>(aggregate);
            VersionedReference<SerializableAggregateRoot> deserialized = original.Clone();

            Assert.Equal(original, deserialized);
            Assert.NotSame(original, deserialized);

            Assert.Equal(aggregate.Id, deserialized.Id);
            Assert.Equal(aggregate.Version, deserialized.Version);
            Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
        }
    }
}