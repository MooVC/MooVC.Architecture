namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Diagnostics;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;

    public abstract class EventReconciler
        : IEventReconciler,
          IEmitDiagnostics
    {
        public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

        public event EventsReconciledAsyncEventHandler? EventsReconciled;

        public event EventsReconcilingAsyncEventHandler? EventsReconciling;

        public event EventSequenceAdvancedAsyncEventHandler? EventSequenceAdvanced;

        public async Task<ulong?> ReconcileAsync(ulong? previous = default, ulong? target = default)
        {
            bool hasEvents;

            do
            {
                (ulong? lastSequence, IEnumerable<DomainEvent> events) = await
                    GetEventsAsync(
                        previous,
                        target: target)
                    .ConfigureAwait(false);

                hasEvents = events.Any();

                if (hasEvents)
                {
                    await ReconcileAsync(events)
                        .ConfigureAwait(false);

                    await OnEventSequenceAdvancedAsync(lastSequence!.Value)
                        .ConfigureAwait(false);

                    previous = lastSequence;
                }
            }
            while (hasEvents);

            return previous;
        }

        protected abstract Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
            ulong? previous,
            ulong? target = default);

        protected abstract Task ReconcileAsync(IEnumerable<DomainEvent> events);

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

        protected virtual Task OnEventsReconciledAsync(IEnumerable<DomainEvent> events)
        {
            return EventsReconciled.PassiveInvokeAsync(
                this,
                new EventReconciliationEventArgs(events),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: EventReconcilerOnEventsReconciledAsyncFailure));
        }

        protected virtual Task OnEventsReconcilingAsync(IEnumerable<DomainEvent> events)
        {
            return EventsReconciling.InvokeAsync(this, new EventReconciliationEventArgs(events));
        }

        protected virtual Task OnEventSequenceAdvancedAsync(ulong current)
        {
            return EventSequenceAdvanced.PassiveInvokeAsync(
                this,
                new EventSequenceAdvancedEventArgs(current),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: EventReconcilerOnEventSequenceAdvancedAsyncFailure));
        }
    }
}