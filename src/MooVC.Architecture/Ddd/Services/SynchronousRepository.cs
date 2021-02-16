namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousRepository<TAggregate>
        : Repository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public override Task<IEnumerable<TAggregate>> GetAllAsync()
        {
            return Task.FromResult(PerformGetAll());
        }

        public override Task<TAggregate?> GetAsync(Guid id, SignedVersion? version = default)
        {
            return Task.FromResult(PerformGet(id, version: version));
        }

        protected override Task<VersionedReference?> GetCurrentVersionAsync(TAggregate aggregate)
        {
            return Task.FromResult(PerformGetCurrentVersion(aggregate));
        }

        protected override Task UpdateStoreAsync(TAggregate aggregate)
        {
            PerformUpdateStore(aggregate);

            return Task.CompletedTask;
        }

        protected abstract IEnumerable<TAggregate> PerformGetAll();

        protected abstract TAggregate? PerformGet(Guid id, SignedVersion? version = default);

        protected abstract VersionedReference? PerformGetCurrentVersion(TAggregate aggregate);

        protected abstract void PerformUpdateStore(TAggregate aggregate);
    }
}