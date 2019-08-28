namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using static Ensure;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public abstract TAggregate Get(Guid id, ulong? version = null);

        public abstract IEnumerable<TAggregate> GetAll();

        public void Save(TAggregate aggregate)
        {
            ulong? currentVersion = GetCurrentVersion(aggregate.Id);

            AggregateDoesNotConflict<TAggregate>(aggregate, currentVersion: currentVersion);

            PerformSave(aggregate);
        }

        protected abstract ulong? GetCurrentVersion(Guid id);

        protected abstract void PerformSave(TAggregate aggregate);
    }
}