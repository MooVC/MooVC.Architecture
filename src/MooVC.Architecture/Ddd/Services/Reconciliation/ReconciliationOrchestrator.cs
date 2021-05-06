namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Threading.Tasks;

    public abstract class ReconciliationOrchestrator
        : IReconciliationOrchestrator
    {
        public event SnapshotRestorationCommencingEventHandler? SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedEventHandler? SnapshotRestorationCompleted;

        public abstract Task ReconcileAsync(IEventSequence? target = default);

        protected virtual void OnSnapshotRestorationCommencing()
        {
            SnapshotRestorationCommencing?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSnapshotRestorationCompleted(IEventSequence sequence)
        {
            SnapshotRestorationCompleted?.Invoke(
                this,
                new SnapshotRestorationCompletedEventArgs(sequence));
        }
    }
}