namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public interface IEventReconciler
    {
        public event EventsReconciledEventHandler EventsReconciled;

        public event EventsReconcilingEventHandler EventsReconciling;

        public event EventSequenceAdvancedEventHandler EventSequenceAdvanced;

        ulong? Reconcile(ulong? previous = default, ulong? target = default);
    }
}