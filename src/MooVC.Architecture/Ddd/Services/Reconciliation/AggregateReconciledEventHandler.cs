namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public delegate void AggregateReconciledEventHandler(IAggregateReconciler sender, AggregateReconciledEventArgs e);
}