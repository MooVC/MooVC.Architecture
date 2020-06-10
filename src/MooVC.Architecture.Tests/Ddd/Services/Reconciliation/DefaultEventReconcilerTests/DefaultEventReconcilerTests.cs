namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultEventReconcilerTests
{
    using MooVC.Persistence;
    using Moq;

    public abstract class DefaultEventReconcilerTests
    {
        protected DefaultEventReconcilerTests()
        {
            EventStore = new Mock<IEventStore<ISequencedEvents, ulong>>();
            Reconciler = new Mock<IAggregateReconciler>();
        }

        protected Mock<IEventStore<ISequencedEvents, ulong>> EventStore { get; }

        protected Mock<IAggregateReconciler> Reconciler { get; }
    }
}