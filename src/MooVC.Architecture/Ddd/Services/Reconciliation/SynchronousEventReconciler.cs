namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousEventReconciler
        : EventReconciler
    {
        protected override Task ReconcileAsync(IEnumerable<DomainEvent> events)
        {
            PerformReconcile(events);

            return Task.CompletedTask;
        }

        protected abstract void PerformReconcile(IEnumerable<DomainEvent> events);
    }
}