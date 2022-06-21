namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd;
using MooVC.Diagnostics;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;

public abstract class AggregateReconciler
    : IAggregateReconciler,
      IEmitDiagnostics
{
    public event AggregateConflictDetectedAsyncEventHandler? AggregateConflictDetected;

    public event AggregateReconciledAsyncEventHandler? AggregateReconciled;

    public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

    public event UnsupportedAggregateTypeDetectedAsyncEventHandler? UnsupportedAggregateTypeDetected;

    public virtual Task ReconcileAsync(EventCentricAggregateRoot aggregate, CancellationToken? cancellationToken = default)
    {
        return ReconcileAsync(new[] { aggregate }, cancellationToken: cancellationToken);
    }

    public abstract Task ReconcileAsync(IEnumerable<EventCentricAggregateRoot> aggregates, CancellationToken? cancellationToken = default);

    public virtual Task ReconcileAsync(DomainEvent @event, CancellationToken? cancellationToken = default)
    {
        return ReconcileAsync(new[] { @event }, cancellationToken: cancellationToken);
    }

    public abstract Task ReconcileAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default);

    protected virtual async Task<bool> EventsAreNonConflictingAsync(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        CancellationToken? cancellationToken = default)
    {
        IEnumerable<SignedVersion> versions = events
            .Select(@event => @event.Aggregate.Version)
            .Distinct();

        SignedVersion previous = versions.First();

        foreach (SignedVersion next in versions.Skip(1))
        {
            if (!next.IsNext(previous))
            {
                await
                    OnAggregateConflictDetectedAsync(
                        aggregate,
                        events,
                        next,
                        previous,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                return false;
            }
        }

        return true;
    }

    protected virtual Task OnAggregateConflictDetectedAsync(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        SignedVersion next,
        SignedVersion previous,
        CancellationToken? cancellationToken = default)
    {
        return AggregateConflictDetected.InvokeAsync(
            this,
            new AggregateConflictDetectedAsyncEventArgs(
                aggregate,
                events,
                next,
                previous,
                cancellationToken: cancellationToken));
    }

    protected virtual Task OnAggregateReconciledAsync(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        CancellationToken? cancellationToken = default)
    {
        return AggregateReconciled.PassiveInvokeAsync(
            this,
            new AggregateReconciledAsyncEventArgs(aggregate, events, cancellationToken: cancellationToken),
            onFailure: failure => OnDiagnosticsEmittedAsync(
                Level.Warning,
                cancellationToken: cancellationToken,
                cause: failure,
                message: AggregateReconcilerOnAggregateReconciledAsyncFailure));
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

    protected virtual Task OnUnsupportedAggregateTypeDetectedAsync(Type type, CancellationToken? cancellationToken = default)
    {
        return UnsupportedAggregateTypeDetected.InvokeAsync(
            this,
            new UnsupportedAggregateTypeDetectedAsyncEventArgs(type, cancellationToken: cancellationToken));
    }

    protected virtual IEnumerable<DomainEvent> RemovePreviousVersions(IEnumerable<DomainEvent> events, SignedVersion version)
    {
        return events
            .Where(@event => @event.Aggregate.Version.CompareTo(version) > 0)
            .ToArray();
    }

    protected virtual async Task ApplyAsync(
        EventCentricAggregateRoot aggregate,
        IEnumerable<DomainEvent> events,
        IAggregateReconciliationProxy proxy,
        Reference reference,
        CancellationToken? cancellationToken = default)
    {
        if (events.Any())
        {
            try
            {
                aggregate.LoadFromHistory(events);

                await proxy
                    .SaveAsync(aggregate, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                await OnAggregateReconciledAsync(reference, events, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (AggregateHistoryInvalidForStateException invalid)
            {
                await
                    OnAggregateConflictDetectedAsync(
                        invalid.Aggregate,
                        events,
                        invalid.StartingVersion,
                        invalid.Aggregate.Version,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (AggregateConflictDetectedException conflict)
            {
                await
                    OnAggregateConflictDetectedAsync(
                        reference,
                        events,
                        conflict.Received,
                        conflict.Persisted,
                        cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}