namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public delegate Task SnapshotRestorationCompletedAsyncEventHandler(
        IReconciliationOrchestrator orchestrator,
        SnapshotRestorationCompletedAsyncEventArgs e);
}