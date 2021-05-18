namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture;
    using MooVC.Architecture.Ddd;
    using MooVC.Architecture.Services;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public abstract class CoordinatedGenerateHandler<TAggregate, TCommand>
        : IHandler<TCommand>
        where TAggregate : AggregateRoot
        where TCommand : Message
    {
        private readonly IRepository<TAggregate> repository;
        private readonly TimeSpan? timeout;

        protected CoordinatedGenerateHandler(IRepository<TAggregate> repository, TimeSpan? timeout = default)
        {
            ArgumentNotNull(repository, nameof(repository), CoordinatedGenerateHandlerRepositoryRequired);

            this.repository = repository;
            this.timeout = timeout;
        }

        public virtual Task ExecuteAsync(TCommand command, CancellationToken cancellationToken)
        {
            return typeof(TAggregate)
                .CoordinateAsync(
                    () => PerformCoordinatedExecuteAsync(command, cancellationToken),
                    cancellationToken: cancellationToken,
                    timeout: timeout);
        }

        protected abstract TAggregate PerformCoordinatedGenerate(TCommand command);

        protected virtual async Task PerformCoordinatedExecuteAsync(TCommand command, CancellationToken cancellationToken)
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