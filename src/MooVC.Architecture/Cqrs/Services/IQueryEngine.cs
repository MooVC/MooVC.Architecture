namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public interface IQueryEngine
    {
        Task<TResult> QueryAsync<TResult>()
            where TResult : Message;

        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query)
            where TQuery : Message
            where TResult : Message;
    }
}