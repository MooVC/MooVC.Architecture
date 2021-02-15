namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousQueryHandler<TQuery, TResult>
        : IQueryHandler<TQuery, TResult>
        where TQuery : Message
        where TResult : Message
    {
        public virtual async Task<TResult> ExecuteAsync(TQuery query)
        {
            return await Task.FromResult(PerformExecute(query));
        }

        protected abstract TResult PerformExecute(TQuery query);
    }
}