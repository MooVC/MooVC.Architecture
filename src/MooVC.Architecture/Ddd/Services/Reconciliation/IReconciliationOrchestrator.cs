namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;

    public interface IReconciliationOrchestrator
    {
        event SnapshotRestorationCommencingEventHandler SnapshotRestorationCommencing;

        event SnapshotRestorationCompletedEventHandler SnapshotRestorationCompleted;

        IEventSequence Reconcile(IEventSequence target = default);
    }
}