namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.Ddd.Threading;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public abstract class CoordinatedGenerateHandler<TAggregate, TMessage>
    : CoordinatedHandler<TAggregate, TMessage>
    where TAggregate : AggregateRoot
    where TMessage : Message
{
    private readonly IRepository<TAggregate> repository;

    protected CoordinatedGenerateHandler(IAggregateCoordinator<TAggregate> coordinator, IRepository<TAggregate> repository)
        : base(coordinator)
    {
        this.repository = ArgumentNotNull(repository, nameof(repository), CoordinatedGenerateHandlerRepositoryRequired);
    }

    protected virtual TAggregate? Generate(TMessage message)
    {
        return default;
    }

    protected virtual Task<TAggregate?> GenerateAsync(TMessage message, CancellationToken cancellationToken)
    {
        TAggregate? aggregate = Generate(message);

        return Task.FromResult(aggregate);
    }

    protected override async Task PerformExecuteAsync(Reference<TAggregate> context, TMessage message, CancellationToken cancellationToken)
    {
        TAggregate? aggregate = await GenerateAsync(message, cancellationToken);

        if (aggregate is { })
        {
            await VerifyAsync(aggregate, message, cancellationToken)
                .ConfigureAwait(false);

            await SaveAsync(aggregate, message, repository, cancellationToken)
                .ConfigureAwait(false);
        }
    }

    protected virtual Task SaveAsync(
        TAggregate aggregate,
        TMessage message,
        IRepository<TAggregate> repository,
        CancellationToken cancellationToken)
    {
        return repository.SaveAsync(aggregate, cancellationToken: cancellationToken);
    }

    protected virtual Task VerifyAsync(TAggregate aggregate, TMessage message, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}