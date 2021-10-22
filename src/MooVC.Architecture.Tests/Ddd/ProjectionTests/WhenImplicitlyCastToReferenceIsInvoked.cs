namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using Xunit;

    public sealed class WhenImplicitlyCastToReferenceIsInvoked
    {
        [Fact]
        public void GivenAProjectionThenTheReferenceForItsAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var projection = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            Reference reference = projection;

            Assert.True(reference.IsMatch(aggregate));
        }
    }
}