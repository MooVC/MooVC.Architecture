namespace MooVC.Architecture.Ddd.Services.ProjectorTests
{
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.ProjectionTests;
    using Xunit;

    public sealed class WhenProjectAsyncIsCalled
    {
        [Fact]
        public async Task GivenASingleAggregateThenASingleProjectionIsReturnedAsync()
        {
            var projector = new TestableProjector<SerializableAggregateRoot>();
            var aggregate = new SerializableAggregateRoot();

            SerializableProjection<SerializableAggregateRoot> projection = await projector
                .ProjectAsync(aggregate);

            Assert.True(projection.Aggregate.IsMatch(aggregate));
        }
    }
}