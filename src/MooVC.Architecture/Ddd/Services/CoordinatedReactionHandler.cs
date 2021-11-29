namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public abstract class CoordinatedReactionHandler<TAggregate, TEvent>
        : CoordinatedOperationHandler<TAggregate, TEvent>
        where TAggregate : AggregateRoot
        where TEvent : DomainEvent<TAggregate>
    {
        protected CoordinatedReactionHandler(IRepository<TAggregate> repository, TimeSpan? timeout = default)
            : base(repository, timeout)
        {
        }

        protected override Reference<TAggregate> IdentifyTarget(TEvent @event)
        {
            return @event.Aggregate;
        }
    }
}