namespace MooVC.Architecture.Ddd.Services.SynchronousProjectorTests;

using System.Collections.Generic;
using System.Linq;
using MooVC.Architecture.Ddd.ProjectionTests;

public sealed class TestableSynchronousProjector<TAggregate>
    : SynchronousProjector<TAggregate, SerializableProjection<TAggregate>>
    where TAggregate : AggregateRoot
{
    protected override IEnumerable<SerializableProjection<TAggregate>> PerformProject(IEnumerable<TAggregate> aggregates)
    {
        return aggregates
            .Select(aggregate => new SerializableProjection<TAggregate>(aggregate))
            .ToArray();
    }
}