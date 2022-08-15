namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IQueryEngine
{
    Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken? cancellationToken = default)
        where TQuery : Message
        where TResult : Message;
}