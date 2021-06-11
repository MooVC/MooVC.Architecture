namespace MooVC.Architecture.Ddd.Services.Snapshots.DefaultSnapshotProviderTests
{
    using System;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Persistence;
    using Moq;
    using Xunit;

    public sealed class WhenDefaultSnapshotProviderIsConstructed
    {
        private readonly Mock<IAggregateFactory> factory;
        private readonly Mock<IAggregateReconciliationProxy> proxy;
        private readonly Mock<IEventStore<SequencedEvents, ulong>> store;

        public WhenDefaultSnapshotProviderIsConstructed()
        {
            factory = new Mock<IAggregateFactory>();
            proxy = new Mock<IAggregateReconciliationProxy>();
            store = new Mock<IEventStore<SequencedEvents, ulong>>();
        }

        [Fact]
        public void GivenAnEventStoreAFactoryAndAProxyThenAnInstanceIsCreated()
        {
            _ = new DefaultSnapshotProvider<SequencedEvents>(
                store.Object,
                factory.Object,
                () => type => proxy.Object);
        }

        [Fact]
        public void GivenAnEventStoreAFactoryAndANullProxyThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultSnapshotProvider<SequencedEvents>(
                    store.Object,
                    factory.Object,
                    default!));
        }

        [Fact]
        public void GivenAnEventStoreANullFactoryAndAProxyThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultSnapshotProvider<SequencedEvents>(
                    store.Object,
                    default!,
                    () => type => proxy.Object));
        }

        [Fact]
        public void GivenAFactoryAProxyAndANullEventStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultSnapshotProvider<SequencedEvents>(
                    default!,
                    factory.Object,
                    () => type => proxy.Object));
        }
    }
}