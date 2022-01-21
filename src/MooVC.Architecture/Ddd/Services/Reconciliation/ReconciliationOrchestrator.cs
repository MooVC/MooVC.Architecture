namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Diagnostics;

    public abstract class ReconciliationOrchestrator
        : IReconciliationOrchestrator
    {
        public event SnapshotRestorationCommencingAsyncEventHandler? SnapshotRestorationCommencing;

        public event SnapshotRestorationCompletedAsyncEventHandler? SnapshotRestorationCompleted;

        public abstract Task ReconcileAsync(
            CancellationToken? cancellationToken = default,
            IEventSequence? target = default);

        protected virtual Task OnSnapshotRestorationCommencingAsync(CancellationToken? cancellationToken = default)
        {
            return SnapshotRestorationCommencing.InvokeAsync(
                this,
                AsyncEventArgs.Empty(cancellationToken: cancellationToken));
        }

        protected virtual Task OnSnapshotRestorationCompletedAsync(
            IEventSequence sequence,
            CancellationToken? cancellationToken = default)
        {
            return SnapshotRestorationCompleted.InvokeAsync(
                this,
                new SnapshotRestorationCompletedAsyncEventArgs(sequence, cancellationToken: cancellationToken));
        }
    }
}