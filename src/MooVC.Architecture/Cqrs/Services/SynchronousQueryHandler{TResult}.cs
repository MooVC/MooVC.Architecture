namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SynchronousQueryHandler<TResult>
        : IQueryHandler<TResult>
        where TResult : Message
    {
        public virtual Task<TResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(PerformExecute());
        }

        protected abstract TResult PerformExecute();
    }
}