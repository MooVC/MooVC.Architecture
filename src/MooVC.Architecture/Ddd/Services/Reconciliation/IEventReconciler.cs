namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading;
using System.Threading.Tasks;

public interface IEventReconciler
{
    public event EventsReconciledAsyncEventHandler EventsReconciled;

    public event EventsReconcilingAsyncEventHandler EventsReconciling;

    public event EventSequenceAdvancedAsyncEventHandler EventSequenceAdvanced;

    Task<ulong?> ReconcileAsync(CancellationToken? cancellationToken = default, ulong? previous = default, ulong? target = default);
}