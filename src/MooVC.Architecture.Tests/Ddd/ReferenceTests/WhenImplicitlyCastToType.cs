namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;

    public sealed class WhenImplicitlyCastToType
    {
        [Fact]
        public void GivenATypedReferenceThenTheIdOfThatReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = aggregate.ToReference();
            Type type = reference;

            Assert.Equal(reference.Type, type);
            Assert.Equal(typeof(SerializableAggregateRoot), type);
        }

        [Fact]
        public void GivenAnUntypedReferenceThenTheIdOfThatReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = Reference.Create(aggregate);
            Type type = reference;

            Assert.Equal(reference.Type, type);
            Assert.Equal(typeof(SerializableAggregateRoot), type);
        }
    }
}