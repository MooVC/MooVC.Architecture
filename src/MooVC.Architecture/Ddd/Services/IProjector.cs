namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProjector<TAggregate, TProjection>
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        Task<TProjection> ProjectAsync(TAggregate aggregate);

        Task<IEnumerable<TProjection>> ProjectAsync(IEnumerable<TAggregate> aggregates);
    }
}