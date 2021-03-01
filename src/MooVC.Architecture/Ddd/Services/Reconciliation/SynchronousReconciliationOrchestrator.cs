namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public abstract class SynchronousReconciliationOrchestrator
        : ReconciliationOrchestrator
    {
        public override Task ReconcileAsync(IEventSequence? target = default)
        {
            PerformReconcile(target: target);

            return Task.CompletedTask;
        }

        protected abstract void PerformReconcile(IEventSequence? target = default);
    }
}