namespace MooVC.Architecture.Ddd.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public sealed class DomainEventPropagator<TAggregate>
    where TAggregate : EventCentricAggregateRoot
{
    private readonly IBus bus;
    private readonly IRepository<TAggregate> repository;

    public DomainEventPropagator(IBus bus, IRepository<TAggregate> repository)
    {
        this.bus = ArgumentNotNull(
            bus,
            nameof(bus),
            DomainEventPropagatorBusRequired);

        this.repository = ArgumentNotNull(
            repository,
            nameof(repository),
            DomainEventPropagatorRepositoryRequired);

        this.repository.AggregateSaved += Repository_AggregateSaved;
    }

    private async Task Repository_AggregateSaved(
        IRepository<TAggregate> sender,
        AggregateSavedAsyncEventArgs<TAggregate> e)
    {
        IEnumerable<DomainEvent> changes = e.Aggregate.GetUncommittedChanges();

        await bus
            .PublishAsync(changes, cancellationToken: e.CancellationToken)
            .ConfigureAwait(false);
    }
}