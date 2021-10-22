namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;

    public sealed class WhenImplicitlyCastToGuid
    {
        [Fact]
        public void GivenAnEmptyReferenceThenAnEmptyGuidIsReturned()
        {
            Reference reference = Reference<SerializableAggregateRoot>.Empty;
            Guid id = reference;

            Assert.Equal(reference.Id, id);
        }

        [Fact]
        public void GivenAnReferenceThenTheIdOfThatReferenceIsReturned()
        {
            var reference = Reference.Create<SerializableAggregateRoot>(Guid.NewGuid());
            Guid id = reference;

            Assert.Equal(reference.Id, id);
        }
    }
}