namespace MooVC.Architecture.Ddd.Threading;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Threading;
using static MooVC.Architecture.Ddd.Threading.Resources;
using static MooVC.Ensure;

public sealed class AggregateCoordinator<TAggregate>
    : IAggregateCoordinator<TAggregate>
    where TAggregate : AggregateRoot
{
    private readonly ICoordinator<Guid> coordinator;

    public AggregateCoordinator(ICoordinator<Guid> coordinator)
    {
        this.coordinator = ArgumentNotNull(coordinator, nameof(coordinator), AggregateCoordinatorCoordinatorRequired);
    }

    public Task<ICoordinationContext<Guid>> ApplyAsync(CancellationToken? cancellationToken = default, TimeSpan? timeout = default)
    {
        return coordinator.ApplyAsync(Guid.Empty, cancellationToken: cancellationToken, timeout: timeout);
    }

    public Task<ICoordinationContext<Guid>> ApplyAsync(
        TAggregate context,
        CancellationToken? cancellationToken = default,
        TimeSpan? timeout = default)
    {
        return coordinator.ApplyAsync(context.Id, cancellationToken: cancellationToken, timeout: timeout);
    }

    public Task<ICoordinationContext<Guid>> ApplyAsync(
        Reference<TAggregate> context,
        CancellationToken? cancellationToken = default,
        TimeSpan? timeout = default)
    {
        return coordinator.ApplyAsync(context.Id, cancellationToken: cancellationToken, timeout: timeout);
    }
}