namespace MooVC.Architecture.Ddd.Services.DomainEventPropagatorTests
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Moq;
    using Xunit;

    public sealed class WhenAggregateSavedIsRaised
    {
        [Fact]
        public void GivenAnAggregateWithChangesWhenAggregateSavedIsRaisedThenTheChangesArePropagatedToTheBus()
        {
            const int ExpectedTotalChanges = 2;

            var context = new SerializableMessage();
            var aggregate = new SerializableEventCentricAggregateRoot(context);
            var request = new SetRequest(context, Guid.NewGuid());
            var bus = new Mock<IBus>();
            var cloner = new BinaryFormatterCloner();
            var repository = new MemoryRepository<SerializableEventCentricAggregateRoot>(cloner);
            var propagator = new DomainEventPropagator<SerializableEventCentricAggregateRoot>(bus.Object, repository);
            var changes = new DomainEvent[0];

            _ = bus
                .Setup(b => b.Publish(It.IsAny<DomainEvent[]>()))
                .Callback<DomainEvent[]>(events => changes = events);

            aggregate.Set(request);
            repository.Save(aggregate);

            _ = Assert.Single(changes.OfType<SerializableCreatedDomainEvent>());
            _ = Assert.Single(changes.OfType<SerializableSetDomainEvent>());
            Assert.Equal(ExpectedTotalChanges, changes.Length);
        }
    }
}