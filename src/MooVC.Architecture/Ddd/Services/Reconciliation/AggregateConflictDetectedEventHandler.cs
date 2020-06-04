namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public delegate void AggregateConflictDetectedEventHandler(IAggregateReconciler sender, AggregateConflictDetectedEventArgs e);
}