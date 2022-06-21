namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

public abstract class CoordinatedOperationHandler<TAggregate, TMessage>
    : CoordinatedContextHandler<TAggregate, TMessage>
    where TAggregate : AggregateRoot
    where TMessage : Message
{
    protected CoordinatedOperationHandler(IRepository<TAggregate> repository, TimeSpan? timeout = default)
        : base(repository, timeout: timeout)
    {
    }

    protected override Task PerformCoordinatedExecuteAsync(TAggregate aggregate, TMessage message, CancellationToken cancellationToken)
    {
        PerformCoordinatedOperation(aggregate, message);

        return Task.CompletedTask;
    }

    protected abstract void PerformCoordinatedOperation(TAggregate aggregate, TMessage message);
}