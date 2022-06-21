namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture.Services;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public abstract class CoordinatedContextHandler<TAggregate, TMessage>
        : IHandler<TMessage>
        where TAggregate : AggregateRoot
        where TMessage : Message
    {
        private readonly IRepository<TAggregate> repository;
        private readonly TimeSpan? timeout;

        protected CoordinatedContextHandler(IRepository<TAggregate> repository, TimeSpan? timeout = default)
        {
            this.repository = ArgumentNotNull(
                repository,
                nameof(repository),
                CoordinatedContextHandlerRepositoryRequired);

            this.timeout = timeout;
        }

        public virtual async Task ExecuteAsync(TMessage message, CancellationToken cancellationToken)
        {
            if (message is { })
            {
                Reference<TAggregate> reference = IdentifyTarget(message);

                if (reference is { } && !reference.IsEmpty)
                {
                    await reference
                        .CoordinateAsync(
                            () => PerformCoordinatedExecuteAsync(message, reference, cancellationToken),
                            cancellationToken: cancellationToken,
                            timeout: timeout)
                        .ConfigureAwait(false);
                }
            }
        }

        protected abstract Reference<TAggregate> IdentifyTarget(TMessage message);

        protected virtual async Task PerformCoordinatedExecuteAsync(
            TMessage message,
            Reference<TAggregate> reference,
            CancellationToken cancellationToken)
        {
            TAggregate aggregate = await reference
                .RetrieveAsync(message, repository, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await PerformCoordinatedExecuteAsync(aggregate, message, cancellationToken)
                .ConfigureAwait(false);

            await aggregate
                .SaveAsync(repository, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        protected abstract Task PerformCoordinatedExecuteAsync(
            TAggregate aggregate,
            TMessage message,
            CancellationToken cancellationToken);
    }
}