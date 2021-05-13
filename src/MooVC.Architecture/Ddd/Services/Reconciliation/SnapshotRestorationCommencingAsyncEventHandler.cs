namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Threading.Tasks;

    public delegate Task SnapshotRestorationCommencingAsyncEventHandler(
        IReconciliationOrchestrator orchestrator,
        EventArgs e);
}