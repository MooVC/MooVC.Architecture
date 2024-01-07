namespace MooVC.Architecture.Cqrs.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IHandler<T>
    where T : Message
{
    Task ExecuteAsync(T message, CancellationToken cancellationToken);
}