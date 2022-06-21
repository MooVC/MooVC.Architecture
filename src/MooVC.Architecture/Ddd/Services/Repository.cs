namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Threading;
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

    public abstract Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken? cancellationToken = default);

    public abstract Task<TAggregate?> GetAsync(Guid id, CancellationToken? cancellationToken = default, SignedVersion? version = default);

    public virtual async Task SaveAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        await OnAggregateSavingAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await PerformSaveAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await OnAggregateSavedAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        aggregate.MarkChangesAsCommitted();
    }

    protected virtual async Task<bool> CheckForConflictsAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        Reference<TAggregate>? currentVersion = await GetCurrentVersionAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (aggregate.Version == currentVersion?.Version)
        {
            return false;
        }

        AggregateDoesNotConflict(aggregate, currentVersion: currentVersion?.Version);

        return true;
    }

    protected abstract Task<Reference<TAggregate>?> GetCurrentVersionAsync(TAggregate aggregate, CancellationToken? cancellationToken = default);

    protected virtual Task OnAggregateSavedAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return AggregateSaved.PassiveInvokeAsync(
            this,
            new AggregateSavedAsyncEventArgs<TAggregate>(aggregate, cancellationToken: cancellationToken),
            onFailure: failure => OnDiagnosticsEmittedAsync(
                Level.Warning,
                cancellationToken: cancellationToken,
                cause: failure,
                message: RepositoryOnAggregateSavedAsyncFailure));
    }

    protected virtual Task OnAggregateSavingAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return AggregateSaving.InvokeAsync(
            this,
            new AggregateSavingAsyncEventArgs<TAggregate>(aggregate, cancellationToken: cancellationToken));
    }

    protected virtual Task OnDiagnosticsEmittedAsync(
        Level level,
        CancellationToken? cancellationToken = default,
        Exception? cause = default,
        string? message = default)
    {
        return DiagnosticsEmitted.PassiveInvokeAsync(
            this,
            new DiagnosticsEmittedAsyncEventArgs(
                cancellationToken: cancellationToken,
                cause: cause,
                level: level,
                message: message));
    }

    protected virtual async Task PerformSaveAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        bool isConflictFree = await
            CheckForConflictsAsync(
                aggregate,
                cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (isConflictFree)
        {
            await
                UpdateStoreAsync(
                    aggregate,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }

    protected abstract Task UpdateStoreAsync(TAggregate aggregate, CancellationToken? cancellationToken = default);
}