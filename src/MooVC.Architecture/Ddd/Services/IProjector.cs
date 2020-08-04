namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;

    public interface IProjector<TAggregate, TProjection>
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        TProjection Project(TAggregate aggregate);

        IEnumerable<TProjection> Project(IEnumerable<TAggregate> aggregates);
    }
}