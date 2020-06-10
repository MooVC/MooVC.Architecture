namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;

    public interface IAggregateReconciler
    {
        event AggregateConflictDetectedEventHandler AggregateConflictDetected;

        event AggregateReconciledEventHandler AggregateReconciled;

        event UnsupportedAggregateTypeDetectedEventHandler UnsupportedAggregateTypeDetected;

        void Reconcile(params EventCentricAggregateRoot[] aggregates);

        void Reconcile(IEnumerable<DomainEvent> events);
    }
}