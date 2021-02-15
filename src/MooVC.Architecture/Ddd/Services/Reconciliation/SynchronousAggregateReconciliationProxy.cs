namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousAggregateReconciliationProxy
        : IAggregateReconciliationProxy
    {
        public virtual async Task<EventCentricAggregateRoot> CreateAsync(Reference aggregate)
        {
            return await Task.FromResult(PerformCreate(aggregate));
        }

        public virtual async Task<IEnumerable<EventCentricAggregateRoot>> GetAllAsync()
        {
            return await Task.FromResult(PerformGetAll());
        }

        public virtual async Task<EventCentricAggregateRoot?> GetAsync(Reference aggregate)
        {
            return await Task.FromResult(PerformGet(aggregate));
        }

        public virtual async Task OverwriteAsync(EventCentricAggregateRoot aggregate)
        {
            PerformOverwrite(aggregate);

            await Task.CompletedTask;
        }

        public virtual async Task PurgeAsync(Reference aggregate)
        {
            PerformPurge(aggregate);

            await Task.CompletedTask;
        }

        public virtual async Task SaveAsync(EventCentricAggregateRoot aggregate)
        {
            PerformSave(aggregate);

            await Task.CompletedTask;
        }

        protected abstract EventCentricAggregateRoot PerformCreate(Reference aggregate);

        protected abstract IEnumerable<EventCentricAggregateRoot> PerformGetAll();

        protected abstract EventCentricAggregateRoot? PerformGet(Reference aggregate);

        protected abstract void PerformOverwrite(EventCentricAggregateRoot aggregate);

        protected abstract void PerformPurge(Reference aggregate);

        protected abstract void PerformSave(EventCentricAggregateRoot aggregate);
    }
}