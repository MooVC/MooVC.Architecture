namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Reference;

    public sealed class WhenIsEmptyIsCalled
    {
        [Fact]
        public void GivenAnEmptyReferenceThenTheResponseIsPositive()
        {
            Reference<SerializableAggregateRoot> reference = Reference<SerializableAggregateRoot>.Empty;

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
            Reference reference = Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(reference.IsEmpty());
        }
    }
}