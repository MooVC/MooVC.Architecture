namespace MooVC.Architecture.Ddd.Services.DefaultEventReconcilerTests
{
    using MooVC.Persistence;
    using Moq;

    public abstract class DefaultEventReconcilerTests
    {
        protected DefaultEventReconcilerTests()
        {
            EventStore = new Mock<IEventStore<SequencedEvents, ulong>>();
            Reconciler = new Mock<IAggregateReconciler>();
            SequenceStore = new Mock<IStore<EventSequence, ulong>>();
        }

        protected Mock<IEventStore<SequencedEvents, ulong>> EventStore { get; }

        protected Mock<IAggregateReconciler> Reconciler { get; }

        protected Mock<IStore<EventSequence, ulong>> SequenceStore { get; }
    }
}