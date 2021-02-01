namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenSignedVersionIsSerialized
    {
        [Fact]
        public void GivenASignedVersionThenAllPropertiesArePropagated()
        {
            var aggregate = new SerializableAggregateRoot();
            SignedVersion version = aggregate.Version;
            SignedVersion clone = version.Clone();

            Assert.NotSame(version, clone);
            Assert.Equal(version, clone);
        }
    }
}
