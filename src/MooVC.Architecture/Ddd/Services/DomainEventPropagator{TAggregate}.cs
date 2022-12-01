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
        this.bus = IsNotNull(bus, message: DomainEventPropagatorBusRequired);
        this.repository = IsNotNull(repository, message: DomainEventPropagatorRepositoryRequired);

        this.repository.Saved += Repository_Saved;
    }

    private async Task Repository_Saved(IRepository<TAggregate> sender, AggregateSavedAsyncEventArgs<TAggregate> e)
    {
        IEnumerable<DomainEvent> changes = e.Aggregate.GetUncommittedChanges();

        await bus
            .PublishAsync(changes, cancellationToken: e.CancellationToken)
            .ConfigureAwait(false);
    }
}