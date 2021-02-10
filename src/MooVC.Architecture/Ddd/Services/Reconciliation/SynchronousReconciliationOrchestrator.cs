namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading.Tasks;

    public abstract class SynchronousReconciliationOrchestrator
        : ReconciliationOrchestrator
    {
        public override async Task ReconcileAsync(IEventSequence? target = default)
        {
            PerformReconcile(target: target);

            await Task.CompletedTask;
        }

        protected abstract void PerformReconcile(IEventSequence? target = default);
    }
}