namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IEventReconciler
    {
        public event EventsReconciledAsyncEventHandler EventsReconciled;

        public event EventsReconcilingAsyncEventHandler EventsReconciling;

        public event EventSequenceAdvancedAsyncEventHandler EventSequenceAdvanced;

        Task<ulong?> ReconcileAsync(ulong? previous = default, ulong? target = default);
    }
}