namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousAggregateReconciliationProxy
        : IAggregateReconciliationProxy
    {
        public virtual Task<EventCentricAggregateRoot> CreateAsync(Reference aggregate)
        {
            return Task.FromResult(PerformCreate(aggregate));
        }

        public virtual Task<IEnumerable<EventCentricAggregateRoot>> GetAllAsync()
        {
            return Task.FromResult(PerformGetAll());
        }

        public virtual Task<EventCentricAggregateRoot?> GetAsync(Reference aggregate)
        {
            return Task.FromResult(PerformGet(aggregate));
        }

        public virtual Task OverwriteAsync(EventCentricAggregateRoot aggregate)
        {
            PerformOverwrite(aggregate);

            return Task.CompletedTask;
        }

        public virtual Task PurgeAsync(Reference aggregate)
        {
            PerformPurge(aggregate);

            return Task.CompletedTask;
        }

        public virtual Task SaveAsync(EventCentricAggregateRoot aggregate)
        {
            PerformSave(aggregate);

            return Task.CompletedTask;
        }

        protected abstract EventCentricAggregateRoot PerformCreate(Reference aggregate);

        protected abstract IEnumerable<EventCentricAggregateRoot> PerformGetAll();

        protected abstract EventCentricAggregateRoot? PerformGet(Reference aggregate);

        protected abstract void PerformOverwrite(EventCentricAggregateRoot aggregate);

        protected abstract void PerformPurge(Reference aggregate);

        protected abstract void PerformSave(EventCentricAggregateRoot aggregate);
    }
}