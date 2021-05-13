namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IAggregateReconciler
    {
        event AggregateConflictDetectedAsyncEventHandler AggregateConflictDetected;

        event AggregateReconciledAsyncEventHandler AggregateReconciled;

        event UnsupportedAggregateTypeDetectedAsyncEventHandler UnsupportedAggregateTypeDetected;

        Task ReconcileAsync(params EventCentricAggregateRoot[] aggregates);

        Task ReconcileAsync(params DomainEvent[] events);
    }
}