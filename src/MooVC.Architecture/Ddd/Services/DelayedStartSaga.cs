namespace MooVC.Architecture.Ddd.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Processing;

public abstract class DelayedStartSaga<T>
    : TimedJobQueue<T>,
      IStartSaga<T>
    where T : DomainEvent
{
    protected DelayedStartSaga(TimedProcessor timer)
        : base(timer)
    {
    }

    public Task StartAsync(T @event, CancellationToken cancellationToken)
    {
        Enqueue(@event);

        return Task.CompletedTask;
    }
}