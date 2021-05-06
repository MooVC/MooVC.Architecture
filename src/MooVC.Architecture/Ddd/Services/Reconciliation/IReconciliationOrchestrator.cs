namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IReconciliationOrchestrator
    {
        event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        Task ReconcileAsync(IEventSequence? target = default);
    }
}