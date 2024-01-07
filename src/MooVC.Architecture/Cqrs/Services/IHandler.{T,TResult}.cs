namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IHandler<T, TResult>
    where T : Message
    where TResult : Message
{
    Task<TResult> ExecuteAsync(T message, CancellationToken cancellationToken);
}