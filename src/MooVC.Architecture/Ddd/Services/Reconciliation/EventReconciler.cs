namespace MooVC.Architecture.Ddd.Services.Reconciliation;

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

    public event EventsReconciliationAbortedAsyncEventHandler? Aborted;

    public event EventsReconciledAsyncEventHandler? Reconciled;

    public event EventsReconcilingAsyncEventHandler? Reconciling;

    public event EventSequenceAdvancedAsyncEventHandler? SequenceAdvanced;

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

                await OnSequenceAdvancedAsync(lastSequence!.Value, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                previous = lastSequence;
            }
        }
        while (hasEvents);

        return previous;
    }

    protected virtual Task OnAbortedAsync(IEnumerable<DomainEvent> events, Exception reason, CancellationToken? cancellationToken = default)
    {
        return Aborted.PassiveInvokeAsync(
            this,
            new EventsReconciliationAbortedAsyncEventArgs(events, reason, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: EventReconcilerOnAbortedAsyncFailure));
    }

    protected abstract Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
        ulong? previous,
        CancellationToken? cancellationToken = default,
        ulong? target = default);

    protected abstract Task ReconcileAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default);

    protected virtual Task OnReconciledAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        return Reconciled.PassiveInvokeAsync(
            this,
            new EventsReconciliationAsyncEventArgs(events, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: EventReconcilerOnReconciledAsyncFailure));
    }

    protected virtual Task OnReconcilingAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        return Reconciling.InvokeAsync(
            this,
            new EventsReconciliationAsyncEventArgs(events, cancellationToken: cancellationToken));
    }

    protected virtual Task OnSequenceAdvancedAsync(ulong current, CancellationToken? cancellationToken = default)
    {
        return SequenceAdvanced.PassiveInvokeAsync(
            this,
            new EventSequenceAdvancedAsyncEventArgs(current, cancellationToken: cancellationToken),
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: EventReconcilerOnSequenceAdvancedAsyncFailure));
    }
}