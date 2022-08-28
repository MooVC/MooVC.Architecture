namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.Ddd.Threading;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public abstract class CoordinatedGenerateHandler<TAggregate, TCommand>
    : CoordinatedHandler<TAggregate, TCommand>
    where TAggregate : AggregateRoot
    where TCommand : Message
{
    private readonly IRepository<TAggregate> repository;

    protected CoordinatedGenerateHandler(IAggregateCoordinator<TAggregate> coordinator, IRepository<TAggregate> repository)
        : base(coordinator)
    {
        this.repository = ArgumentNotNull(repository, nameof(repository), CoordinatedGenerateHandlerRepositoryRequired);
    }

    protected abstract TAggregate Generate(TCommand command);

    protected override async Task PerformExecuteAsync(Reference<TAggregate> context, TCommand command, CancellationToken cancellationToken)
    {
        TAggregate aggregate = Generate(command);

        await VerifyAsync(aggregate, command, cancellationToken)
            .ConfigureAwait(false);

        await SaveAsync(aggregate, command, repository, cancellationToken)
            .ConfigureAwait(false);
    }

    protected virtual Task SaveAsync(
        TAggregate aggregate,
        TCommand context,
        IRepository<TAggregate> repository,
        CancellationToken cancellationToken)
    {
        return repository.SaveAsync(aggregate, cancellationToken: cancellationToken);
    }

    protected virtual Task VerifyAsync(TAggregate aggregate, TCommand context, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}