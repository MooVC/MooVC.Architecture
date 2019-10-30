namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using System;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAVersionedReferenceThenAReferenceIsReturned()
        {
            var versioned = new VersionedReference<AggregateRoot>(Guid.NewGuid());
            var nonVersioned = versioned.ToReference();

            Assert.True(nonVersioned == versioned);
            Assert.NotSame(versioned, nonVersioned);
        }
    }
}