namespace MooVC.Architecture.Ddd.Services;

using MooVC.Architecture.Ddd.Threading;

public abstract class CoordinatedReactionHandler<TAggregate, TEvent>
    : CoordinatedContextHandler<TAggregate, TEvent>
    where TAggregate : AggregateRoot
    where TEvent : DomainEvent<TAggregate>
{
    protected CoordinatedReactionHandler(IAggregateCoordinator<TAggregate> coordinator, IRepository<TAggregate> repository)
        : base(coordinator, repository)
    {
    }

    protected override Reference<TAggregate> IdentifyCoordinationContext(TEvent @event)
    {
        return @event.Aggregate;
    }
}