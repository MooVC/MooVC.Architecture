namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading;
using System.Threading.Tasks;

public interface IReconciliationOrchestrator
{
    event SnapshotRestorationCommencingAsyncEventHandler SnapshotRestorationCommencing;

    event SnapshotRestorationCompletedAsyncEventHandler SnapshotRestorationCompleted;

    Task ReconcileAsync(
        CancellationToken? cancellationToken = default,
        IEventSequence? target = default);
}