namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.Ddd.Threading;
using MooVC.Architecture.Services;
using MooVC.Threading;
using static Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public abstract class CoordinatedHandler<TAggregate, TMessage>
    : IHandler<TMessage>
    where TAggregate : AggregateRoot
    where TMessage : Message
{
    private readonly IAggregateCoordinator<TAggregate> coordinator;

    protected CoordinatedHandler(IAggregateCoordinator<TAggregate> coordinator)
    {
        this.coordinator = ArgumentNotNull(coordinator, nameof(coordinator), CoordinatedHandlerCoordinatorRequired);
    }

    public virtual async Task ExecuteAsync(TMessage message, CancellationToken cancellationToken)
    {
        Reference<TAggregate> context = await IdentifyCoordinationContextAsync(message, cancellationToken)
            .ConfigureAwait(false);

        ICoordinationContext<Guid> coordination = await coordinator
            .ApplyAsync(context, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        using (coordination)
        {
            await PerformExecuteAsync(context, message, cancellationToken)
                .ConfigureAwait(false);
        }
    }

    protected virtual Reference<TAggregate> IdentifyCoordinationContext(TMessage message)
    {
        if (message is Message<TAggregate> contextual)
        {
            return contextual.Aggregate;
        }

        if (message is DomainEvent @event && @event.Aggregate.Is(out Reference<TAggregate>? source))
        {
            return source;
        }

        return Reference<TAggregate>.Empty;
    }

    protected virtual Task<Reference<TAggregate>> IdentifyCoordinationContextAsync(TMessage message, CancellationToken cancellationToken)
    {
        Reference<TAggregate> context = IdentifyCoordinationContext(message);

        return Task.FromResult(context);
    }

    protected abstract Task PerformExecuteAsync(Reference<TAggregate> context, TMessage message, CancellationToken cancellationToken);
}