namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousQueryEngine
        : IQueryEngine
    {
        public virtual async Task<TResult> QueryAsync<TResult>()
            where TResult : Message
        {
            return await Task.FromResult(PerformQuery<TResult>());
        }

        public virtual async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : Message
            where TResult : Message
        {
            return await Task.FromResult(PerformQuery<TQuery, TResult>(query));
        }

        protected abstract TResult PerformQuery<TResult>()
            where TResult : Message;

        protected abstract TResult PerformQuery<TQuery, TResult>(TQuery query)
            where TQuery : Message
            where TResult : Message;
    }
}