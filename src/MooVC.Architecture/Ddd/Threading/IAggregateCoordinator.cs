namespace MooVC.Architecture.Ddd.Threading;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Threading;

public interface IAggregateCoordinator<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<ICoordinationContext<Guid>> ApplyAsync(CancellationToken? cancellationToken = default, TimeSpan? timeout = default);

    Task<ICoordinationContext<Guid>> ApplyAsync(TAggregate context, CancellationToken? cancellationToken = default, TimeSpan? timeout = default);

    Task<ICoordinationContext<Guid>> ApplyAsync(
        Reference<TAggregate> context,
        CancellationToken? cancellationToken = default,
        TimeSpan? timeout = default);
}