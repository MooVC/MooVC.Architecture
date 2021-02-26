namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Services.Ensure;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public event AggregateSavedEventHandler<TAggregate>? AggregateSaved;

        public event AggregateSavingEventHandler<TAggregate>? AggregateSaving;

        public abstract Task<IEnumerable<TAggregate>> GetAllAsync();

        public abstract Task<TAggregate?> GetAsync(Guid id, SignedVersion? version = default);

        public virtual async Task SaveAsync(TAggregate aggregate)
        {
            OnAggregateSaving(aggregate);

            await PerformSaveAsync(aggregate)
                .ConfigureAwait(false);

            OnAggregateSaved(aggregate);

            aggregate.MarkChangesAsCommitted();
        }

        protected virtual async Task<bool> CheckForConflictsAsync(TAggregate aggregate)
        {
            Reference<TAggregate>? currentVersion = await GetCurrentVersionAsync(aggregate)
                .ConfigureAwait(false);

            if (aggregate.Version == currentVersion?.Version)
            {
                return false;
            }

            AggregateDoesNotConflict(aggregate, currentVersion: currentVersion?.Version);

            return true;
        }

        protected abstract Task<Reference<TAggregate>?> GetCurrentVersionAsync(TAggregate aggregate);

        protected void OnAggregateSaved(TAggregate aggregate)
        {
            AggregateSaved?.Invoke(this, new AggregateSavedEventArgs<TAggregate>(aggregate));
        }

        protected void OnAggregateSaving(TAggregate aggregate)
        {
            AggregateSaving?.Invoke(this, new AggregateSavingEventArgs<TAggregate>(aggregate));
        }

        protected virtual async Task PerformSaveAsync(TAggregate aggregate)
        {
            bool isConflictFree = await CheckForConflictsAsync(aggregate)
                .ConfigureAwait(false);

            if (isConflictFree)
            {
                await UpdateStoreAsync(aggregate)
                    .ConfigureAwait(false);
            }
        }

        protected abstract Task UpdateStoreAsync(TAggregate aggregate);
    }
}