namespace MooVC.Architecture.Ddd.Services.ProjectorTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.ProjectionTests;

    public sealed class TestableProjector<TAggregate>
        : Projector<TAggregate, SerializableProjection<TAggregate>>
        where TAggregate : AggregateRoot
    {
        public override IEnumerable<SerializableProjection<TAggregate>> Project(IEnumerable<TAggregate> aggregates)
        {
            return aggregates
                .Select(aggregate => new SerializableProjection<TAggregate>(aggregate))
                .ToArray();
        }
    }
}