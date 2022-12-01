namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : Message
    where TResult : Message
{
    Task<TResult> ExecuteAsync(TQuery query, CancellationToken cancellationToken);
}