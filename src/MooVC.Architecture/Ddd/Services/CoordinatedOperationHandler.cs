namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Services;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public abstract class CoordinatedOperationHandler<TAggregate, TMessage>
        : IHandler<TMessage>
        where TAggregate : AggregateRoot
        where TMessage : Message
    {
        private readonly IRepository<TAggregate> repository;
        private readonly TimeSpan? timeout;

        public CoordinatedOperationHandler(IRepository<TAggregate> repository, TimeSpan? timeout = default)
        {
            ArgumentNotNull(repository, nameof(repository), CoordinatedOperationHandlerRepositoryRequired);

            this.repository = repository;
            this.timeout = timeout;
        }

        public virtual async Task ExecuteAsync(TMessage message)
        {
            if (message is { })
            {
                Reference<TAggregate> reference = IdentifyTarget(message);

                if (reference is { } && !reference.IsEmpty)
                {
                    await reference
                        .CoordinateAsync(
                            () => PerformCoordinatedExecuteAsync(message, reference),
                            timeout: timeout)
                        .ConfigureAwait(false);
                }
            }
        }

        protected abstract Reference<TAggregate> IdentifyTarget(TMessage message);

        protected virtual async Task PerformCoordinatedExecuteAsync(TMessage message, Reference<TAggregate> reference)
        {
            TAggregate aggregate = reference.Retrieve(message, repository);

            PerformCoordinatedOperation(aggregate, message);

            aggregate.Save(repository);

            // TODO: Await Save

            await Task.CompletedTask;
        }

        protected abstract void PerformCoordinatedOperation(TAggregate aggregate, TMessage message);
    }
}