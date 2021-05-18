namespace MooVC.Architecture.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SynchronousHandler<TMessage>
        : IHandler<TMessage>
        where TMessage : Message
    {
        public virtual Task ExecuteAsync(TMessage message, CancellationToken cancellationToken)
        {
            PerformExecute(message);

            return Task.CompletedTask;
        }

        protected abstract void PerformExecute(TMessage message);
    }
}