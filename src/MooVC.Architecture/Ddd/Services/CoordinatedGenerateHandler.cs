namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture;
    using MooVC.Architecture.Ddd;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public abstract class CoordinatedGenerateHandler<TAggregate, TCommand>
        : CoordinatedHandler<TAggregate, TCommand>
        where TAggregate : AggregateRoot
        where TCommand : Message
    {
        private readonly IRepository<TAggregate> repository;

        protected CoordinatedGenerateHandler(
            IRepository<TAggregate> repository,
            TimeSpan? timeout = default)
            : base(timeout: timeout)
        {
            this.repository = ArgumentNotNull(
                repository,
                nameof(repository),
                CoordinatedGenerateHandlerRepositoryRequired);
        }

        protected abstract TAggregate PerformCoordinatedGenerate(TCommand command);

        protected override async Task PerformCoordinatedExecuteAsync(TCommand command, CancellationToken cancellationToken)
        {
            TAggregate aggregate = PerformCoordinatedGenerate(command);

            await PerformSupplementalActivitiesAsync(aggregate, command, cancellationToken)
                .ConfigureAwait(false);

            await repository
                .SaveAsync(aggregate, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        protected virtual Task PerformSupplementalActivitiesAsync(
            TAggregate aggregate,
            TCommand context,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}