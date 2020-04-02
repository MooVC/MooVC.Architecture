namespace MooVC.Architecture.Ddd.Services
{
    public delegate void AggregateConflictDetectedEventHandler(IAggregateReconciler sender, AggregateConflictDetectedEventArgs e);
}