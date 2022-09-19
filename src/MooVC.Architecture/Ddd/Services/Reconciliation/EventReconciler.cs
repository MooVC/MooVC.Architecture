namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Diagnostics;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;

public abstract class EventReconciler
    : IEventReconciler,
      IEmitDiagnostics
{
    protected EventReconciler(IDiagnosticsProxy? diagnostics = default)
    {
        Diagnostics = new DiagnosticsRelay(this, diagnostics: diagnostics);
    }

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => Diagnostics.DiagnosticsEmitted += value;
        remove => Diagnostics.DiagnosticsEmitted -= value;
    }

    public event EventsReconciledAsyncEventHandler? EventsReconciled;

    public event EventsReconcilingAsyncEventHandler? EventsReconciling;

    public event EventSequenceAdvancedAsyncEventHandler? EventSequenceAdvanced;

    protected IDiagnosticsRelay Diagnostics { get; }

    public async Task<ulong?> ReconcileAsync(CancellationToken? cancellationToken = default, ulong? previous = default, ulong? target = default)
    {
        bool hasEvents;

        do
        {
            (ulong? lastSequence, IEnumerable<DomainEvent> events) = await
                GetEventsAsync(previous, cancellationToken: cancellationToken, target: target)
                .ConfigureAwait(false);

            hasEvents = events.Any();

            if (hasEvents)
            {
                await ReconcileAsync(events, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                await OnEventSequenceAdvancedAsync(lastSequence!.Value, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                previous = lastSequence;
            }
        }
        while (hasEvents);

        return previous;
    }

    protected abstract Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
        ulong? previous,
        CancellationToken? cancellationToken = default,
        ulong? target = default);

    protected abstract Task ReconcileAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default);

    protected virtual Task OnEventsReconciledAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        return EventsReconciled.PassiveInvokeAsync(
            this,
            new EventReconciliationAsyncEventArgs(events, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: EventReconcilerOnEventsReconciledAsyncFailure));
    }

    protected virtual Task OnEventsReconcilingAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        return EventsReconciling.InvokeAsync(
            this,
            new EventReconciliationAsyncEventArgs(events, cancellationToken: cancellationToken));
    }

    protected virtual Task OnEventSequenceAdvancedAsync(ulong current, CancellationToken? cancellationToken = default)
    {
        return EventSequenceAdvanced.PassiveInvokeAsync(
            this,
            new EventSequenceAdvancedAsyncEventArgs(current, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: EventReconcilerOnEventSequenceAdvancedAsyncFailure));
    }
}