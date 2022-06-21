namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousReconciliationOrchestratorTests;

using System;

public sealed class TestableSynchronousReconciliationOrchestrator
    : SynchronousReconciliationOrchestrator
{
    private readonly Action<IEventSequence?>? reconciler;

    public TestableSynchronousReconciliationOrchestrator(Action<IEventSequence?>? reconciler = default)
    {
        this.reconciler = reconciler;
    }

    protected override void PerformReconcile(IEventSequence? target = default)
    {
        if (reconciler is null)
        {
            throw new NotImplementedException();
        }

        reconciler(target);
    }
}