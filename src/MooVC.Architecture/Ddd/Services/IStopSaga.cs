namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IStopSaga<T>
    where T : DomainEvent
{
    Task StopAsync(T @event, CancellationToken cancellationToken);
}