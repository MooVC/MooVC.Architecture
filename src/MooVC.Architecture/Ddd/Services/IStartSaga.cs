namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IStartSaga<T>
    where T : DomainEvent
{
    Task StartAsync(T @event, CancellationToken cancellationToken);
}