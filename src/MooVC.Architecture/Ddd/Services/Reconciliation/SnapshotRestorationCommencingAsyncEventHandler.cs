namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task SnapshotRestorationCommencingAsyncEventHandler(
    IReconciliationOrchestrator orchestrator,
    AsyncEventArgs e);