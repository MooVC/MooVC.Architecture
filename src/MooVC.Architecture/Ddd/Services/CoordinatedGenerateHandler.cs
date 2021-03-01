namespace MooVC.Architecture.Ddd.Services
{
    using System;
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

        public virtual Task ExecuteAsync(TCommand command)
        {
            return typeof(TAggregate)
                .CoordinateAsync(
                    () => PerformCoordinatedExecuteAsync(command),
                    timeout: timeout);
        }

        protected abstract TAggregate PerformCoordinatedGenerate(TCommand command);

        protected virtual Task PerformCoordinatedExecuteAsync(TCommand command)
        {
            TAggregate aggregate = PerformCoordinatedGenerate(command);

            return repository.SaveAsync(aggregate);
        }
    }
}