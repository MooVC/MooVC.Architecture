namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SynchronousAggregateReconciliationProxy
        : DefaultAggregateFactory,
          IAggregateReconciliationProxy
    {
        public override Task<EventCentricAggregateRoot> CreateAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default)
        {
            return Task.FromResult(PerformCreate(aggregate));
        }

        public virtual Task<IEnumerable<EventCentricAggregateRoot>> GetAllAsync(
            CancellationToken? cancellationToken = default)
        {
            return Task.FromResult(PerformGetAll());
        }

        public virtual Task<EventCentricAggregateRoot?> GetAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default)
        {
            return Task.FromResult(PerformGet(aggregate));
        }

        public virtual Task OverwriteAsync(
            EventCentricAggregateRoot aggregate,
            CancellationToken? cancellationToken = default)
        {
            PerformOverwrite(aggregate);

            return Task.CompletedTask;
        }

        public virtual Task PurgeAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default)
        {
            PerformPurge(aggregate);

            return Task.CompletedTask;
        }

        public virtual Task SaveAsync(
            EventCentricAggregateRoot aggregate,
            CancellationToken? cancellationToken = default)
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