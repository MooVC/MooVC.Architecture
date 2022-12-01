namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Threading;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public abstract class CoordinatedContextHandler<TAggregate, TMessage>
    : CoordinatedHandler<TAggregate, TMessage>
    where TAggregate : AggregateRoot
    where TMessage : Message
{
    private readonly IRepository<TAggregate> repository;

    protected CoordinatedContextHandler(IAggregateCoordinator<TAggregate> coordinator, IRepository<TAggregate> repository)
        : base(coordinator)
    {
        this.repository = IsNotNull(repository, message: CoordinatedContextHandlerRepositoryRequired);
    }

    protected abstract Task PerformExecuteAsync(TAggregate aggregate, TMessage message, CancellationToken cancellationToken);

    protected override async Task PerformExecuteAsync(Reference<TAggregate> context, TMessage message, CancellationToken cancellationToken)
    {
        TAggregate aggregate = await RetrieveAsync(context, message, repository, cancellationToken)
            .ConfigureAwait(false);

        await PerformExecuteAsync(aggregate, message, cancellationToken)
            .ConfigureAwait(false);

        await SaveAsync(aggregate, message, repository, cancellationToken)
            .ConfigureAwait(false);
    }

    protected virtual Task<TAggregate> RetrieveAsync(
        Reference<TAggregate> context,
        TMessage message,
        IRepository<TAggregate> repository,
        CancellationToken cancellationToken)
    {
        return context.RetrieveAsync(message, repository, cancellationToken: cancellationToken);
    }

    protected virtual Task SaveAsync(TAggregate aggregate, TMessage message, IRepository<TAggregate> repository, CancellationToken cancellationToken)
    {
        return aggregate.SaveAsync(repository, cancellationToken: cancellationToken);
    }
}