namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public interface IEventReconciler
    {
        public event EventsReconciledEventHandler EventsReconciled;

        public event EventsReconcilingEventHandler EventsReconciling;

        public event EventSequenceAdvancedEventHandler EventSequenceAdvanced;

        Task<ulong?> ReconcileAsync(ulong? previous = default, ulong? target = default);
    }
}