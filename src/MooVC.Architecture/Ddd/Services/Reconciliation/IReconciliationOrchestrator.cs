namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public interface IReconciliationOrchestrator
    {
        event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        void Reconcile(IEventSequence? target = default);
    }
}