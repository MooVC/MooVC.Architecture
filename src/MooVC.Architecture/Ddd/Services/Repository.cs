namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public abstract TAggregate Get(Guid id, ulong? version = null);

        public abstract IEnumerable<TAggregate> GetAll();

        public void Save(TAggregate aggregate)
        {
            ulong currentVersion = GetCurrentVersion(aggregate.Id);

            if (aggregate.Version - currentVersion != 1)
            {
                if (currentVersion == ulong.MinValue)
                {
                    throw new AggregateConflictDetectedException<TAggregate>(aggregate.Id, aggregate.Version);
                }

                throw new AggregateConflictDetectedException<TAggregate>(aggregate.Id, currentVersion, aggregate.Version);
            }

            PerformSave(aggregate);
        }

        protected abstract ulong GetCurrentVersion(Guid id);

        protected abstract void PerformSave(TAggregate aggregate);
    }
}