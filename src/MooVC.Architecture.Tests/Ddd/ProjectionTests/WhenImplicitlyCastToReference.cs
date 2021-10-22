namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using Xunit;

    public sealed class WhenImplicitlyCastToReference
    {
        [Fact]
        public void GivenAProjectionWhenCastToAnUntypedReferenceThenTheUntypedReferenceForItsAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var projection = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            Reference reference = projection;

            Assert.True(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenAProjectionWhenCastToAnTypedReferenceThenTheTypedReferenceForItsAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var projection = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            Reference<SerializableAggregateRoot> reference = projection;

            Assert.True(reference.IsMatch(aggregate));
        }
    }
}