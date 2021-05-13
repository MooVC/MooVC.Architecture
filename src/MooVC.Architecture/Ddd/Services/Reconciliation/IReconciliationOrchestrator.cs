namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IReconciliationOrchestrator
    {
        event SnapshotRestorationCommencingAsyncEventHandler SnapshotRestorationCommencing;

        event SnapshotRestorationCompletedAsyncEventHandler SnapshotRestorationCompleted;

        Task ReconcileAsync(IEventSequence? target = default);
    }
}