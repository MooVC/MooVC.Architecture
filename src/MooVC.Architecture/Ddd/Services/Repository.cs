namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using static Ensure;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public event AggregateSavedEventHandler<TAggregate> AggregateSaved;

        public event AggregateSavingEventHandler<TAggregate> AggregateSaving;

        public abstract TAggregate Get(Guid id, ulong? version = default);

        public abstract IEnumerable<TAggregate> GetAll();

        public void Save(TAggregate aggregate)
        {
            ulong? currentVersion = GetCurrentVersion(aggregate.Id);

            AggregateDoesNotConflict<TAggregate>(aggregate, currentVersion: currentVersion);

            PerformSave(aggregate);
        }

        protected abstract ulong? GetCurrentVersion(Guid id);

        protected abstract void PerformSave(TAggregate aggregate);

        protected void OnAggregateSaved(TAggregate aggregate)
        {
            AggregateSaved?.Invoke(this, new AggregateSavedEventArgs<TAggregate>(aggregate));
        }

        protected void OnAggregateSaving(TAggregate aggregate)
        {
            AggregateSaving?.Invoke(this, new AggregateSavingEventArgs<TAggregate>(aggregate));
        }
    }
}