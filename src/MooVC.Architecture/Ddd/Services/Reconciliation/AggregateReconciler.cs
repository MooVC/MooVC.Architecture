namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public abstract Task ReconcileAsync(params EventCentricAggregateRoot[] aggregates);

        public abstract Task ReconcileAsync(params DomainEvent[] events);

        protected virtual async Task<bool> EventsAreNonConflictingAsync(
            Reference aggregate,
            IEnumerable<DomainEvent> events)
        {
            IEnumerable<SignedVersion> versions = events
                .Select(@event => @event.Aggregate.Version)
                .Distinct();

            SignedVersion previous = versions.First();

            foreach (SignedVersion next in versions.Skip(1))
            {
                if (!next.IsNext(previous))
                {
                    await OnAggregateConflictDetectedAsync(aggregate, events, next, previous)
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
            SignedVersion previous)
        {
            return AggregateConflictDetected.InvokeAsync(
                this,
                new AggregateConflictDetectedEventArgs(
                    aggregate,
                    events,
                    next,
                    previous));
        }

        protected virtual Task OnAggregateReconciledAsync(Reference aggregate, IEnumerable<DomainEvent> events)
        {
            return AggregateReconciled.PassiveInvokeAsync(
                this,
                new AggregateReconciledEventArgs(aggregate, events),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: AggregateReconcilerOnAggregateReconciledAsyncFailure));
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

        protected virtual Task OnUnsupportedAggregateTypeDetectedAsync(Type type)
        {
            return UnsupportedAggregateTypeDetected.InvokeAsync(
                this,
                new UnsupportedAggregateTypeDetectedEventArgs(type));
        }

        protected virtual IEnumerable<DomainEvent> RemovePreviousVersions(
            IEnumerable<DomainEvent> events,
            SignedVersion version)
        {
            return events
                .Where(@event => @event.Aggregate.Version.CompareTo(version) > 0)
                .ToArray();
        }

        protected virtual async Task ApplyAsync(
            EventCentricAggregateRoot aggregate,
            IEnumerable<DomainEvent> events,
            IAggregateReconciliationProxy proxy,
            Reference reference)
        {
            if (events.Any())
            {
                try
                {
                    aggregate.LoadFromHistory(events);

                    await proxy
                        .SaveAsync(aggregate)
                        .ConfigureAwait(false);

                    await OnAggregateReconciledAsync(reference, events)
                        .ConfigureAwait(false);
                }
                catch (AggregateHistoryInvalidForStateException invalid)
                {
                    await
                        OnAggregateConflictDetectedAsync(
                            invalid.Aggregate,
                            events,
                            invalid.StartingVersion,
                            invalid.Aggregate.Version)
                        .ConfigureAwait(false);
                }
                catch (AggregateConflictDetectedException conflict)
                {
                    await
                        OnAggregateConflictDetectedAsync(
                            reference,
                            events,
                            conflict.Received,
                            conflict.Persisted)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}