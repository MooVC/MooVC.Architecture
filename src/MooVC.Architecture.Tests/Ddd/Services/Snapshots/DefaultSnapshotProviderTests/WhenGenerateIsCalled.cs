namespace MooVC.Architecture.Ddd.Services.Snapshots.DefaultSnapshotProviderTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Architecture.MessageTests;
    using MooVC.Persistence;
    using Moq;
    using Xunit;

    public sealed class WhenGenerateIsCalled
    {
        private readonly Mock<IAggregateReconciliationProxy> proxy;
        private readonly Mock<IEventStore<SequencedEvents, ulong>> store;

        public WhenGenerateIsCalled()
        {
            proxy = new Mock<IAggregateReconciliationProxy>();
            store = new Mock<IEventStore<SequencedEvents, ulong>>();
        }

        [Fact]
        public void GivenAStreamThenTheSnapshotContainsTheAggregatesThatComprizeThatStream()
        {
            SerializableEventCentricAggregateRoot expected = CreateAggregate(out IEnumerable<DomainEvent> events);
            var other = new SerializableEventCentricAggregateRoot(expected.Id);

            _ = proxy
                .Setup(proxy => proxy.Create(It.IsAny<Reference>()))
                .Returns(other);

            _ = proxy
                .Setup(proxy => proxy.Get(It.IsAny<Reference>()))
                .Returns(default(EventCentricAggregateRoot)!);

            _ = proxy
                .Setup(proxy => proxy.GetAll())
                .Returns(new[] { other });

            _ = store
                .Setup(store => store.Read(It.Is<ulong>(lastIndex => lastIndex == ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new[] { new SequencedEvents(1, events.ToArray()) });

            _ = store
                .Setup(store => store.Read(It.Is<ulong>(lastIndex => lastIndex > ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            var instance = new DefaultSnapshotProvider<SequencedEvents>(store.Object, () => type => proxy.Object);
            ISnapshot? snapshot = instance.Generate();

            Assert.NotNull(snapshot);

            EventCentricAggregateRoot actual = Assert.Single(snapshot!.Aggregates);
            Assert.Equal(expected, actual);

            proxy.Verify(proxy => proxy.Create(It.IsAny<Reference>()), times: Times.Once);
            proxy.Verify(proxy => proxy.Create(It.Is<Reference>(reference => reference.Id == expected.Id)), times: Times.Once);
            proxy.Verify(proxy => proxy.Get(It.IsAny<Reference>()), times: Times.Once);
            proxy.Verify(proxy => proxy.GetAll(), times: Times.Once);
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

            _ = proxy
                .Setup(proxy => proxy.Create(It.IsAny<Reference>()))
                .Returns<Reference>(reference => aggregates[reference.Id].Snapshot);

            _ = proxy
                .Setup(proxy => proxy.Get(It.IsAny<Reference>()))
                .Returns(default(EventCentricAggregateRoot)!);

            _ = proxy
                .Setup(proxy => proxy.GetAll())
                .Returns(new[] { firstSnapshot, secondSnapshot });

            _ = store
                .Setup(store => store.Read(It.IsAny<ulong>(), It.IsAny<ushort>()))
                .Returns<ulong, ushort>((lastIndex, numberToRead) => new[]
                {
                    new SequencedEvents(lastIndex + 1, aggregates.Values.ElementAt((int)lastIndex).Events),
                });

            var instance = new DefaultSnapshotProvider<SequencedEvents>(
                store.Object,
                () => type => proxy.Object,
                numberToRead: 1);

            _ = instance.Generate(target: 2);

            Assert.Equal(first, firstSnapshot);
            Assert.Equal(second, secondSnapshot);
            Assert.NotEqual(third, thirdSnapshot);
        }

        private SerializableEventCentricAggregateRoot CreateAggregate(out IEnumerable<DomainEvent> @events)
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