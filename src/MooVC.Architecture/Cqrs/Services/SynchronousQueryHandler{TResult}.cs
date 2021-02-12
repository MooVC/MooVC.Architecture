namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousQueryHandler<TResult>
        : IQueryHandler<TResult>
        where TResult : Message
    {
        public virtual async Task<TResult> ExecuteAsync()
        {
            return await Task.FromResult(PerformExecute());
        }

        protected abstract TResult PerformExecute();
    }
}