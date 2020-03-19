namespace MooVC.Architecture.Ddd.Services
{
    public delegate void AggregateReconciledEventHandler(IAggregateReconciler sender, AggregateReconciledEventArgs e);
}