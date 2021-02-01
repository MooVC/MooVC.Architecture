namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenVersionedReferenceIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);
            VersionedReference<SerializableAggregateRoot> clone = reference.Clone();

            Assert.Equal(reference, clone);
            Assert.NotSame(reference, clone);

            Assert.Equal(aggregate.Id, clone.Id);
            Assert.Equal(aggregate.Version, clone.Version);
            Assert.Equal(reference.GetHashCode(), clone.GetHashCode());
        }
    }
}