namespace MooVC.Architecture.Ddd.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Collections.Generic;

public abstract class Projector<TAggregate, TProjection>
    : IProjector<TAggregate, TProjection>
    where TAggregate : AggregateRoot
    where TProjection : Projection<TAggregate>
{
    public async Task<TProjection> ProjectAsync(TAggregate aggregate, CancellationToken? cancellationToken = default, Message? context = default)
    {
        IEnumerable<TProjection> projections = await ProjectAsync(aggregate.AsEnumerable(), cancellationToken: cancellationToken, context: context)
            .ConfigureAwait(false);

        return projections.Single();
    }

    public abstract Task<IEnumerable<TProjection>> ProjectAsync(
        IEnumerable<TAggregate> aggregates,
        CancellationToken? cancellationToken = default,
        Message? context = default);
}