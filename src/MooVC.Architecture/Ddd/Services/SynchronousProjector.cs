namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousProjector<TAggregate, TProjection>
        : Projector<TAggregate, TProjection>
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        public override async Task<IEnumerable<TProjection>> ProjectAsync(IEnumerable<TAggregate> aggregates)
        {
            return await Task.FromResult(PerformProject(aggregates));
        }

        protected abstract IEnumerable<TProjection> PerformProject(IEnumerable<TAggregate> aggregates);
    }
}