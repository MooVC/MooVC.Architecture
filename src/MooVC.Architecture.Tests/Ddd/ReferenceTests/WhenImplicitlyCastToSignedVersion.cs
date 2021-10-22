namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using Xunit;

    public sealed class WhenImplicitlyCastToSignedVersion
    {
        [Fact]
        public void GivenAnEmptyReferenceThenAnEmptyVersionIsReturned()
        {
            Reference reference = Reference<SerializableAggregateRoot>.Empty;
            SignedVersion version = reference;

            Assert.Equal(reference.Version, version);
            Assert.True(version.IsEmpty);
        }

        [Fact]
        public void GivenAnReferenceThenTheVersionOfThatReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = aggregate.ToReference();
            SignedVersion version = reference;

            Assert.Equal(reference.Version, version);
            Assert.Equal(aggregate.Version, version);
            Assert.False(version.IsEmpty);
            Assert.True(version.IsNew);
        }

        [Fact]
        public void GivenANullReferenceThenAnEmptyVersionIsReturned()
        {
            Reference? reference = default;
            SignedVersion version = reference;

            Assert.Equal(SignedVersion.Empty, version);
            Assert.True(version.IsEmpty);
        }
    }
}