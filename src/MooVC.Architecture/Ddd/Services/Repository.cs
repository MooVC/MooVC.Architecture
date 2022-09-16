﻿namespace MooVC.Architecture.Ddd.Services;

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
    private readonly IDiagnosticsProxy diagnostics;

    protected Repository(IDiagnosticsProxy? diagnostics = default)
    {
        this.diagnostics = diagnostics ?? new DiagnosticsProxy();
    }

    public event AggregateSavedAsyncEventHandler<TAggregate>? AggregateSaved;

    public event AggregateSavingAsyncEventHandler<TAggregate>? AggregateSaving;

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => diagnostics.DiagnosticsEmitted += value;
        remove => diagnostics.DiagnosticsEmitted -= value;
    }

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
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: RepositoryOnAggregateSavedAsyncFailure));
    }

    protected virtual Task OnAggregateSavingAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return AggregateSaving.InvokeAsync(
            this,
            new AggregateSavingAsyncEventArgs<TAggregate>(aggregate, cancellationToken: cancellationToken));
    }

    protected virtual Task OnDiagnosticsEmittedAsync(
        CancellationToken? cancellationToken = default,
        Exception? cause = default,
        Impact? impact = default,
        Level? level = default,
        string? message = default)
    {
        return diagnostics.EmitAsync(this, cancellationToken: cancellationToken, cause: cause, impact: Impact.None, level: level, message: message);
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