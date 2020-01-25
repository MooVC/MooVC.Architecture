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

        public abstract TAggregate Get(Guid id, SignedVersion version = default);

        public abstract IEnumerable<TAggregate> GetAll();

        public virtual void Save(TAggregate aggregate)
        {
            OnAggregateSaving(aggregate);
            PerformSave(aggregate);
            OnAggregateSaved(aggregate);

            aggregate.MarkChangesAsCommitted();
        }

        protected virtual bool CheckForConflicts(TAggregate aggregate)
        {
            VersionedReference currentVersion = GetCurrentVersion(aggregate);

            if (aggregate.Version == currentVersion?.Version)
            {
                return false;
            }

            AggregateDoesNotConflict<TAggregate>(aggregate, currentVersion: currentVersion?.Version);

            return true;
        }

        protected abstract VersionedReference GetCurrentVersion(TAggregate aggregate);

        protected void OnAggregateSaved(TAggregate aggregate)
        {
            AggregateSaved?.Invoke(this, new AggregateSavedEventArgs<TAggregate>(aggregate));
        }

        protected void OnAggregateSaving(TAggregate aggregate)
        {
            AggregateSaving?.Invoke(this, new AggregateSavingEventArgs<TAggregate>(aggregate));
        }

        protected virtual void PerformSave(TAggregate aggregate)
        {
            if (CheckForConflicts(aggregate))
            {
                UpdateStore(aggregate);
            }
        }

        protected abstract void UpdateStore(TAggregate aggregate);
    }
}