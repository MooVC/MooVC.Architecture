namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IAggregateReconciler
{
    event AggregateConflictDetectedAsyncEventHandler AggregateConflictDetected;

    event AggregateReconciledAsyncEventHandler AggregateReconciled;

    event UnsupportedAggregateTypeDetectedAsyncEventHandler UnsupportedAggregateTypeDetected;

    Task ReconcileAsync(
        EventCentricAggregateRoot aggregate,
        CancellationToken? cancellationToken = default);

    Task ReconcileAsync(
        IEnumerable<EventCentricAggregateRoot> aggregates,
        CancellationToken? cancellationToken = default);

    Task ReconcileAsync(
        DomainEvent @event,
        CancellationToken? cancellationToken = default);

    Task ReconcileAsync(
        IEnumerable<DomainEvent> events,
        CancellationToken? cancellationToken = default);
}