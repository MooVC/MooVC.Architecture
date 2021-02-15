namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IAggregateReconciler
    {
        event AggregateConflictDetectedEventHandler AggregateConflictDetected;

        event AggregateReconciledEventHandler AggregateReconciled;

        event UnsupportedAggregateTypeDetectedEventHandler UnsupportedAggregateTypeDetected;

        Task ReconcileAsync(params EventCentricAggregateRoot[] aggregates);

        Task ReconcileAsync(params DomainEvent[] events);
    }
}