namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public interface IAggregateReconciler
    {
        event AggregateConflictDetectedEventHandler AggregateConflictDetected;

        event AggregateReconciledEventHandler AggregateReconciled;

        event UnsupportedAggregateTypeDetectedEventHandler UnsupportedAggregateTypeDetected;

        void Reconcile(params EventCentricAggregateRoot[] aggregates);

        void Reconcile(params DomainEvent[] events);
    }
}