namespace MooVC.Architecture.Ddd.Services.ProjectorTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.ProjectionTests;

    public sealed class TestableProjector<TAggregate>
        : Projector<TAggregate, SerializableProjection<TAggregate>>
        where TAggregate : AggregateRoot
    {
        public override async Task<IEnumerable<SerializableProjection<TAggregate>>> ProjectAsync(
            IEnumerable<TAggregate> aggregates)
        {
            SerializableProjection<TAggregate>[] projections = aggregates
                .Select(aggregate => new SerializableProjection<TAggregate>(aggregate))
                .ToArray();

            return await Task.FromResult(projections);
        }
    }
}