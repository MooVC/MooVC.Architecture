namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using MooVC.Architecture.Services;
    using static MooVC.Ensure;
    using static Resources;

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

        public virtual void Execute(TMessage message)
        {
            if (message is { })
            {
                Reference<TAggregate> reference = IdentifyTarget(message);

                if (reference is { } && !reference.IsEmpty)
                {
                    reference.Coordinate(
                        () => PerformCoordinatedExecute(message, reference),
                        timeout: timeout);
                }
            }
        }

        protected abstract Reference<TAggregate> IdentifyTarget(TMessage message);

        protected virtual void PerformCoordinatedExecute(TMessage message, Reference<TAggregate> reference)
        {
            TAggregate aggregate = reference.Retrieve(message, repository);

            PerformCoordinatedOperation(aggregate, message);

            aggregate.Save(repository);
        }

        protected abstract void PerformCoordinatedOperation(TAggregate aggregate, TMessage message);
    }
}