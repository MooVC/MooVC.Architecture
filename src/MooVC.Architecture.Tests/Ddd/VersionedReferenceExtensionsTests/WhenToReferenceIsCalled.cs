namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAVersionedReferenceThenAReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var versioned = new VersionedReference<SerializableAggregateRoot>(aggregate);
            var nonVersioned = versioned.ToReference();

            Assert.True(nonVersioned == versioned);
            Assert.NotSame(versioned, nonVersioned);
        }
    }
}