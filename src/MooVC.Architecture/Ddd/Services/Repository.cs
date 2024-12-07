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
    protected Repository(IDiagnosticsProxy? diagnostics = default)
    {
        Diagnostics = new DiagnosticsRelay(this, diagnostics: diagnostics);
    }

    public event AggregateSavingAbortedAsyncEventHandler<TAggregate>? Aborted;

    public event AggregateSavedAsyncEventHandler<TAggregate>? Saved;

    public event AggregateSavingAsyncEventHandler<TAggregate>? Saving;

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => Diagnostics.DiagnosticsEmitted += value;
        remove => Diagnostics.DiagnosticsEmitted -= value;
    }

    protected IDiagnosticsRelay Diagnostics { get; }

    public abstract Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken? cancellationToken = default);

    public abstract Task<TAggregate?> GetAsync(Guid id, CancellationToken? cancellationToken = default, Sequence? version = default);

    public virtual async Task SaveAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        await OnSavingAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        try
        {
            await PerformSaveAsync(aggregate, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await OnAbortedAsync(aggregate, ex, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            throw;
        }

        await OnSavedAsync(aggregate, cancellationToken: cancellationToken)
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

        DoesNotConflict(aggregate, currentVersion: currentVersion?.Version);

        return true;
    }

    protected abstract Task<Reference<TAggregate>?> GetCurrentVersionAsync(TAggregate aggregate, CancellationToken? cancellationToken = default);

    protected virtual Task OnAbortedAsync(TAggregate aggregate, Exception reason, CancellationToken? cancellationToken = default)
    {
        return Aborted.PassiveInvokeAsync(
            this,
            new AggregateSavingAbortedAsyncEventArgs<TAggregate>(aggregate, reason, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: RepositoryOnAbortedAsyncFailure));
    }

    protected virtual Task OnSavedAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return Saved.PassiveInvokeAsync(
            this,
            new AggregateSavedAsyncEventArgs<TAggregate>(aggregate, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: RepositoryOnSavedAsyncFailure));
    }

    protected virtual Task OnSavingAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return Saving.InvokeAsync(
            this,
            new AggregateSavingAsyncEventArgs<TAggregate>(aggregate, cancellationToken: cancellationToken));
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