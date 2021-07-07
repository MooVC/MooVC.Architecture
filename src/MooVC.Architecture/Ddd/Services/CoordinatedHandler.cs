namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture;
    using MooVC.Architecture.Ddd;
    using MooVC.Architecture.Services;

    public abstract class CoordinatedHandler<TAggregate, TCommand>
        : IHandler<TCommand>
        where TAggregate : AggregateRoot
        where TCommand : Message
    {
        private readonly TimeSpan? timeout;

        protected CoordinatedHandler(TimeSpan? timeout = default)
        {
            this.timeout = timeout;
        }

        public virtual Task ExecuteAsync(
            TCommand command,
            CancellationToken cancellationToken)
        {
            return typeof(TAggregate)
                .CoordinateAsync(
                    () => PerformCoordinatedExecuteAsync(command, cancellationToken),
                    cancellationToken: cancellationToken,
                    timeout: timeout);
        }

        protected abstract Task PerformCoordinatedExecuteAsync(
            TCommand command,
            CancellationToken cancellationToken);
    }
}