namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;

    public interface IAggregateReconciler
    {
        event AggregateConflictDetectedEventHandler AggregateConflictDetected;

        event AggregateReconciledEventHandler AggregateReconciled;

        event UnsupportedAggregateDetectedEventHandler UnsupportedAggregateDetected;

        void Reconcile(IEnumerable<DomainEvent> events);
    }
}