namespace MooVC.Architecture.Ddd.Services.ProjectorTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.ProjectionTests;
    using Xunit;

    public sealed class WhenProjectIsCalled
    {
        [Fact]
        public void GivenASingleAggregateThenASingleProjectionIsReturned()
        {
            var projector = new TestableProjector<SerializableAggregateRoot>();
            var aggregate = new SerializableAggregateRoot();
            SerializableProjection<SerializableAggregateRoot> projection = projector.Project(aggregate);

            Assert.True(projection.Aggregate.IsMatch(aggregate));
        }
    }
}