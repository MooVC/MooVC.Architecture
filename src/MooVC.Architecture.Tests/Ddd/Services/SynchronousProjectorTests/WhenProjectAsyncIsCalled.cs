namespace MooVC.Architecture.Ddd.Services.SynchronousProjectorTests
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
            var projector = new TestableSynchronousProjector<SerializableAggregateRoot>();
            var aggregate = new SerializableAggregateRoot();

            SerializableProjection<SerializableAggregateRoot> projection = await projector
                .ProjectAsync(aggregate);

            Assert.True(projection.Aggregate.IsMatch(aggregate));
        }
    }
}