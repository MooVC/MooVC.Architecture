namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Projector<TAggregate, TProjection>
        : IProjector<TAggregate, TProjection>
        where TAggregate : AggregateRoot
        where TProjection : Projection<TAggregate>
    {
        public TProjection Project(TAggregate aggregate)
        {
            return Project(new[] { aggregate }).Single();
        }

        public abstract IEnumerable<TProjection> Project(IEnumerable<TAggregate> aggregates);
    }
}