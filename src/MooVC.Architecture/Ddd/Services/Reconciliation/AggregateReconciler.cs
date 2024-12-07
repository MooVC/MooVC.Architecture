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
    protected AggregateReconciler(IDiagnosticsProxy? diagnostics = default)
    {
        Diagnostics = new DiagnosticsRelay(this, diagnostics: diagnostics);
    }

    public event AggregateConflictDetectedAsyncEventHandler? ConflictDetected;

    public event AggregateReconciledAsyncEventHandler? Reconciled;

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => Diagnostics.DiagnosticsEmitted += value;
        remove => Diagnostics.DiagnosticsEmitted -= value;
    }

    public event UnsupportedAggregateTypeDetectedAsyncEventHandler? UnsupportedTypeDetected;

    protected IDiagnosticsRelay Diagnostics { get; }

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
        IEnumerable<Sequence> versions = events
            .Select(@event => @event.Aggregate.Version)
            .Distinct();

        Sequence previous = versions.First();

        foreach (Sequence next in versions.Skip(1))
        {
            if (!next.IsNext(previous))
            {
                await
                    OnConflictDetectedAsync(
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

    protected virtual Task OnConflictDetectedAsync(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        Sequence next,
        Sequence previous,
        CancellationToken? cancellationToken = default)
    {
        return ConflictDetected.InvokeAsync(
            this,
            new AggregateConflictDetectedAsyncEventArgs(
                aggregate,
                events,
                next,
                previous,
                cancellationToken: cancellationToken));
    }

    protected virtual Task OnReconciledAsync(Reference aggregate, IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        return Reconciled.PassiveInvokeAsync(
            this,
            new AggregateReconciledAsyncEventArgs(aggregate, events, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: AggregateReconcilerOnAggregateReconciledAsyncFailure));
    }

    protected virtual Task OnUnsupportedTypeDetectedAsync(Type type, CancellationToken? cancellationToken = default)
    {
        return UnsupportedTypeDetected.InvokeAsync(
            this,
            new UnsupportedAggregateTypeDetectedAsyncEventArgs(type, cancellationToken: cancellationToken));
    }

    protected virtual IEnumerable<DomainEvent> RemovePreviousVersions(IEnumerable<DomainEvent> events, Sequence version)
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

                await OnReconciledAsync(reference, events, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (AggregateHistoryInvalidForStateException invalid)
            {
                await
                    OnConflictDetectedAsync(
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
                    OnConflictDetectedAsync(
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