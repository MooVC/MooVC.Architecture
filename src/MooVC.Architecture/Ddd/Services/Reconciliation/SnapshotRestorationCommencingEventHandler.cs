namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;

    public delegate void SnapshotRestorationCommencingEventHandler(IReconciliationOrchestrator orchestrator, EventArgs e);
}