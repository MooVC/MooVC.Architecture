namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading;
using System.Threading.Tasks;

public abstract class SynchronousReconciliationOrchestrator
    : ReconciliationOrchestrator
{
    public override Task ReconcileAsync(
        CancellationToken? cancellationToken = default,
        IEventSequence? target = default)
    {
        PerformReconcile(target: target);

        return Task.CompletedTask;
    }

    protected abstract void PerformReconcile(IEventSequence? target = default);
}