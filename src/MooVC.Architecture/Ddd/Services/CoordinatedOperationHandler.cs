namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Threading;

public abstract class CoordinatedOperationHandler<TAggregate, TMessage>
    : CoordinatedContextHandler<TAggregate, TMessage>
    where TAggregate : AggregateRoot
    where TMessage : Message
{
    protected CoordinatedOperationHandler(IAggregateCoordinator<TAggregate> coordinator, IRepository<TAggregate> repository)
        : base(coordinator, repository)
    {
    }

    protected override Task PerformExecuteAsync(TAggregate aggregate, TMessage message, CancellationToken cancellationToken)
    {
        Apply(aggregate, message);

        return Task.CompletedTask;
    }

    protected abstract void Apply(TAggregate aggregate, TMessage message);
}