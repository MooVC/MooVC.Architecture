namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public delegate Task AggregateReconciledAsyncEventHandler(
        IAggregateReconciler sender,
        AggregateReconciledAsyncEventArgs e);
}