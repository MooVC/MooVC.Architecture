namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public delegate Task AggregateConflictDetectedAsyncEventHandler(
        IAggregateReconciler sender,
        AggregateConflictDetectedAsyncEventArgs e);
}