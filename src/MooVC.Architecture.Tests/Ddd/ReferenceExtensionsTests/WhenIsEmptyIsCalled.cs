namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenIsEmptyIsCalled
    {
        [Fact]
        public void GivenAnEmptyReferenceThenTheResponseIsPositive()
        {
            Reference<SerializableAggregateRoot> reference = Reference<SerializableAggregateRoot>.Empty;

            Assert.True(reference.IsEmpty());
        }

        [Fact]
        public void GivenAnEmptyVersionedReferenceThenTheResponseIsPositive()
        {
            VersionedReference<SerializableAggregateRoot> reference = VersionedReference<SerializableAggregateRoot>.Empty;

            Assert.True(reference.IsEmpty());
        }

        [Fact]
        public void GivenANullReferenceThenTheResponseIsPositive()
        {
            Reference? reference = default;

            Assert.True(reference.IsEmpty());
        }

        [Fact]
        public void GivenAReferenceThenTheResponseIsNegative()
        {
            var reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(reference.IsEmpty());
        }

        [Fact]
        public void GivenAVersionedReferenceThenTheResponseIsNegative()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = aggregate.ToVersionedReference();

            Assert.False(reference.IsEmpty());
        }
    }
}