namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousRepository<TAggregate>
        : Repository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public override async Task<IEnumerable<TAggregate>> GetAllAsync()
        {
            return await Task.FromResult(PerformGetAll());
        }

        public override async Task<TAggregate?> GetAsync(Guid id, SignedVersion? version = default)
        {
            return await Task.FromResult(PerformGet(id, version: version));
        }

        protected override async Task<VersionedReference?> GetCurrentVersionAsync(TAggregate aggregate)
        {
            return await Task.FromResult(PerformGetCurrentVersion(aggregate));
        }

        protected override async Task UpdateStoreAsync(TAggregate aggregate)
        {
            PerformUpdateStore(aggregate);

            await Task.CompletedTask;
        }

        protected abstract IEnumerable<TAggregate> PerformGetAll();

        protected abstract TAggregate? PerformGet(Guid id, SignedVersion? version = default);

        protected abstract VersionedReference? PerformGetCurrentVersion(TAggregate aggregate);

        protected abstract void PerformUpdateStore(TAggregate aggregate);
    }
}