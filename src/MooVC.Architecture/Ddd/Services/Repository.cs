namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MooVC.Diagnostics;
    using static MooVC.Architecture.Ddd.Services.Ensure;
    using static MooVC.Architecture.Ddd.Services.Resources;

    public abstract class Repository<TAggregate>
        : IRepository<TAggregate>,
          IEmitDiagnostics
        where TAggregate : AggregateRoot
    {
        public event AggregateSavedAsyncEventHandler<TAggregate>? AggregateSaved;

        public event AggregateSavingAsyncEventHandler<TAggregate>? AggregateSaving;

        public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

        public abstract Task<IEnumerable<TAggregate>> GetAllAsync();

        public abstract Task<TAggregate?> GetAsync(Guid id, SignedVersion? version = default);

        public virtual async Task SaveAsync(TAggregate aggregate)
        {
            await OnAggregateSavingAsync(aggregate)
                .ConfigureAwait(false);

            await PerformSaveAsync(aggregate)
                .ConfigureAwait(false);

            await OnAggregateSavedAsync(aggregate)
                .ConfigureAwait(false);

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

        protected virtual Task OnAggregateSavedAsync(TAggregate aggregate)
        {
            return AggregateSaved.PassiveInvokeAsync(
                this,
                new AggregateSavedEventArgs<TAggregate>(aggregate),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: RepositoryOnAggregateSavedAsyncFailure));
        }

        protected virtual Task OnAggregateSavingAsync(TAggregate aggregate)
        {
            return AggregateSaving.InvokeAsync(this, new AggregateSavingEventArgs<TAggregate>(aggregate));
        }

        protected virtual Task OnDiagnosticsEmittedAsync(
            Level level,
            Exception? cause = default,
            string? message = default)
        {
            return DiagnosticsEmitted.PassiveInvokeAsync(
                this,
                new DiagnosticsEmittedEventArgs(
                    cause: cause,
                    level: level,
                    message: message));
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