namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Projector<TAggregate, TProjection>
        : IProjector<TAggregate, TProjection>
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        public async Task<TProjection> ProjectAsync(TAggregate aggregate)
        {
            IEnumerable<TProjection> projections = await ProjectAsync(new[] { aggregate })
                .ConfigureAwait(false);

            return projections.Single();
        }

        public abstract Task<IEnumerable<TProjection>> ProjectAsync(IEnumerable<TAggregate> aggregates);
    }
}