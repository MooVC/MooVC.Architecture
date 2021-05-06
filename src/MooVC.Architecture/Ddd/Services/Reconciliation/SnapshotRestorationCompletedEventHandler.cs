namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public delegate void SnapshotRestorationCompletedEventHandler(
        IReconciliationOrchestrator orchestrator,
        SnapshotRestorationCompletedEventArgs e);
}