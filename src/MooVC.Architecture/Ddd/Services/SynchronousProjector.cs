namespace MooVC.Architecture.Ddd.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public abstract class SynchronousProjector<TAggregate, TProjection>
    : Projector<TAggregate, TProjection>
    where TAggregate : AggregateRoot
    where TProjection : Projection<TAggregate>
{
    public override Task<IEnumerable<TProjection>> ProjectAsync(IEnumerable<TAggregate> aggregates, CancellationToken? cancellationToken = default)
    {
        return Task.FromResult(PerformProject(aggregates));
    }

    protected abstract IEnumerable<TProjection> PerformProject(IEnumerable<TAggregate> aggregates);
}