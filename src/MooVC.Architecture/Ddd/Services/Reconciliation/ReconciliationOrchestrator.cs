namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Diagnostics;

    public abstract class ReconciliationOrchestrator
        : IReconciliationOrchestrator
    {
        public event SnapshotRestorationCommencingAsyncEventHandler? SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedAsyncEventHandler? SnapshotRestorationCompleted;

        public abstract Task ReconcileAsync(IEventSequence? target = default);

        protected virtual Task OnSnapshotRestorationCommencingAsync()
        {
            return SnapshotRestorationCommencing.InvokeAsync(this, EventArgs.Empty);
        }

        protected virtual Task OnSnapshotRestorationCompletedAsync(IEventSequence sequence)
        {
            return SnapshotRestorationCompleted.InvokeAsync(
                this,
                new SnapshotRestorationCompletedEventArgs(sequence));
        }
    }
}