namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading;
using System.Threading.Tasks;

public interface IEventReconciler
{
    public event EventsReconciliationAbortedAsyncEventHandler Aborted;

    public event EventsReconciledAsyncEventHandler Reconciled;

    public event EventsReconcilingAsyncEventHandler Reconciling;

    public event EventSequenceAdvancedAsyncEventHandler SequenceAdvanced;

    Task<ulong?> ReconcileAsync(CancellationToken? cancellationToken = default, ulong? previous = default, ulong? target = default);
}