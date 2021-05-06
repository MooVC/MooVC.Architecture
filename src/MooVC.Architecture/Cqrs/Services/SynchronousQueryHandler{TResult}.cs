namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousQueryHandler<TResult>
        : IQueryHandler<TResult>
        where TResult : Message
    {
        public virtual Task<TResult> ExecuteAsync()
        {
            return Task.FromResult(PerformExecute());
        }

        protected abstract TResult PerformExecute();
    }
}