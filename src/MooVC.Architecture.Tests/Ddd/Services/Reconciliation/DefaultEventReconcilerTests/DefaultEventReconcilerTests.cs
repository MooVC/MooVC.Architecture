namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultEventReconcilerTests;

using MooVC.Persistence;
using Moq;

public abstract class DefaultEventReconcilerTests
{
    protected DefaultEventReconcilerTests()
    {
        EventStore = new Mock<IEventStore<SequencedEvents, ulong>>();
        Reconciler = new Mock<IAggregateReconciler>();
    }

    protected Mock<IEventStore<SequencedEvents, ulong>> EventStore { get; }

    protected Mock<IAggregateReconciler> Reconciler { get; }
}