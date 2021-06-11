namespace MooVC.Architecture.Ddd.Services.Snapshots.DefaultSnapshotProviderTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Architecture.MessageTests;
    using MooVC.Persistence;
    using Moq;
    using Xunit;

    public sealed class WhenGenerateIsCalled
    {
        private readonly Mock<IAggregateFactory> factory;
        private readonly Mock<IAggregateReconciliationProxy> proxy;
        private readonly Mock<IEventStore<SequencedEvents, ulong>> store;

        public WhenGenerateIsCalled()
        {
            factory = new Mock<IAggregateFactory>();
            proxy = new Mock<IAggregateReconciliationProxy>();
            store = new Mock<IEventStore<SequencedEvents, ulong>>();
        }

        [Fact]
        public async Task GivenAStreamThenTheSnapshotContainsTheAggregatesThatComprizeThatStreamAsync()
        {
            SerializableEventCentricAggregateRoot expected = CreateAggregate(out IEnumerable<DomainEvent> events);
            var other = new SerializableEventCentricAggregateRoot(expected.Id);

            _ = factory
                .Setup(factory => factory.CreateAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()))
                .ReturnsAsync(other);

            _ = proxy
                .Setup(proxy => proxy.GetAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()))
                .ReturnsAsync(default(EventCentricAggregateRoot)!);

            _ = proxy
                .Setup(proxy => proxy.GetAllAsync(It.IsAny<CancellationToken?>()))
                .ReturnsAsync(new[] { other });

            _ = store
                .Setup(store => store.ReadAsync(
                    It.Is<ulong>(lastIndex => lastIndex == ulong.MinValue),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<ushort>()))
                .ReturnsAsync(new[] { new SequencedEvents(1, events.ToArray()) });

            _ = store
                .Setup(store => store.ReadAsync(
                    It.Is<ulong>(lastIndex => lastIndex > ulong.MinValue),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            var instance = new DefaultSnapshotProvider<SequencedEvents>(
                store.Object,
                factory.Object,
                () => type => proxy.Object);

            ISnapshot? snapshot = await instance.GenerateAsync();

            Assert.NotNull(snapshot);

            EventCentricAggregateRoot actual = Assert.Single(snapshot!.Aggregates);
            Assert.Equal(expected, actual);

            factory.Verify(
                factory => factory.CreateAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            factory.Verify(
                factory => factory.CreateAsync(
                    It.Is<Reference>(reference => reference.Id == expected.Id),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            proxy.Verify(
                proxy => proxy.GetAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()),
                times: Times.Once);

            proxy.Verify(
                proxy => proxy.GetAllAsync(It.IsAny<CancellationToken?>()),
                times: Times.Once);
        }

        [Fact]
        public void GivenAStreamWithATargetThenTheSnapshotContainsTheAggregatesToTheTargetPointWithinThatStream()
        {
            SerializableEventCentricAggregateRoot first = CreateAggregate(out IEnumerable<DomainEvent> firstEvents);
            SerializableEventCentricAggregateRoot second = CreateAggregate(out IEnumerable<DomainEvent> secondEvents);
            SerializableEventCentricAggregateRoot third = CreateAggregate(out IEnumerable<DomainEvent> thirdEvents);

            var firstSnapshot = new SerializableEventCentricAggregateRoot(first.Id);
            var secondSnapshot = new SerializableEventCentricAggregateRoot(second.Id);
            var thirdSnapshot = new SerializableEventCentricAggregateRoot(third.Id);

            var aggregates = new Dictionary<Guid, (DomainEvent[] Events, EventCentricAggregateRoot Original, EventCentricAggregateRoot Snapshot)>
            {
                { first.Id, (firstEvents.ToArray(), first, firstSnapshot) },
                { second.Id, (secondEvents.ToArray(), second, secondSnapshot) },
                { third.Id, (thirdEvents.ToArray(), third, thirdSnapshot) },
            };

            _ = factory
                .Setup(factory => factory.CreateAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()))
                .ReturnsAsync<Reference, CancellationToken?, IAggregateFactory, EventCentricAggregateRoot>(
                    (reference, _) => aggregates[reference.Id].Snapshot);

            _ = proxy
                .Setup(proxy => proxy.GetAsync(
                    It.IsAny<Reference>(),
                    It.IsAny<CancellationToken?>()))
                .ReturnsAsync(default(EventCentricAggregateRoot)!);

            _ = proxy
                .Setup(proxy => proxy.GetAllAsync(It.IsAny<CancellationToken?>()))
                .ReturnsAsync(new[] { firstSnapshot, secondSnapshot });

            _ = store
                .Setup(store => store.ReadAsync(
                    It.IsAny<ulong>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<ushort>()))
                .ReturnsAsync<ulong, CancellationToken?, ushort, IEventStore<SequencedEvents, ulong>, IEnumerable<SequencedEvents>>(
                    (lastIndex, _, numberToRead) => new[]
                    {
                        new SequencedEvents(lastIndex + 1, aggregates.Values.ElementAt((int)lastIndex).Events),
                    });

            var instance = new DefaultSnapshotProvider<SequencedEvents>(
                store.Object,
                factory.Object,
                () => type => proxy.Object,
                numberToRead: 1);

            _ = instance.GenerateAsync(target: 2);

            Assert.Equal(first, firstSnapshot);
            Assert.Equal(second, secondSnapshot);
            Assert.NotEqual(third, thirdSnapshot);
        }

        private static SerializableEventCentricAggregateRoot CreateAggregate(out IEnumerable<DomainEvent> @events)
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));
            aggregate.Set(new SetRequest(context, Guid.NewGuid()));
            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            events = aggregate.GetUncommittedChanges();

            aggregate.MarkChangesAsCommitted();

            return aggregate;
        }
    }
}