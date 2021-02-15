namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class SynchronousEventReconciler
        : EventReconciler
    {
        protected override async Task ReconcileAsync(IEnumerable<DomainEvent> events)
        {
            PerformReconcile(events);

            await Task.CompletedTask;
        }

        protected abstract void PerformReconcile(IEnumerable<DomainEvent> events);
    }
}