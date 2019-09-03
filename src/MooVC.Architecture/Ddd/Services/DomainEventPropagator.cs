namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DomainEventPropagator<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : EventCentricAggregateRoot
    {
        private readonly IBus bus;
        private readonly IRepository<TAggregate> repository;

        public DomainEventPropagator(IBus bus, IRepository<TAggregate> repository)
        {
            ArgumentNotNull(bus, nameof(bus), DomainEventPropagatorBusRequired);
            ArgumentNotNull(repository, nameof(repository), DomainEventPropagatorRepositoryRequired);

            this.bus = bus;
            this.repository = repository;
            this.repository.AggregateSaved += Repository_AggregateSaved;
        }

        public event AggregateSavedEventHandler<TAggregate> AggregateSaved
        {
            add { repository.AggregateSaved += value; }
            remove { repository.AggregateSaved -= value; }
        }

        public event AggregateSavingEventHandler<TAggregate> AggregateSaving
        {
            add { repository.AggregateSaving += value; }
            remove { repository.AggregateSaving -= value; }
        }

        public TAggregate Get(Guid id, ulong? version = null)
        {
            return repository.Get(id, version: version);
        }

        public IEnumerable<TAggregate> GetAll()
        {
            return repository.GetAll();
        }

        public void Save(TAggregate aggregate)
        {
            repository.Save(aggregate);
        }

        private void Repository_AggregateSaved(IRepository<TAggregate> sender, AggregateSavedEventArgs<TAggregate> e)
        {
            IEnumerable<DomainEvent> changes = e.Aggregate.GetUncommittedChanges();

            bus.Publish(changes.ToArray());
        }
    }
}