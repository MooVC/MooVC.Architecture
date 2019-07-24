namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public abstract TAggregate Get(Guid id, ulong? version = null);

        public void Save(TAggregate aggregate)
        {
            ulong currentVersion = GetCurrentVersion(aggregate.Id);

            if (currentVersion > aggregate.Version)
            {
                throw new AggregateConflictDetectedException<TAggregate>(aggregate.Id, aggregate.Version, currentVersion);
            }

            PerformSave(aggregate);
        }

        protected abstract ulong GetCurrentVersion(Guid id);

        protected abstract void PerformSave(TAggregate aggregate);
    }
}