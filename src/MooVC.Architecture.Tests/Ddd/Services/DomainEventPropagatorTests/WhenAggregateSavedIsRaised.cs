namespace MooVC.Architecture.Ddd.Services.DomainEventPropagatorTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Moq;
    using Xunit;

    public sealed class WhenAggregateSavedIsRaised
    {
        [Fact]
        public async Task GivenAnAggregateWithChangesWhenAggregateSavedIsRaisedThenTheChangesArePropagatedToTheBusAsync()
        {
            const int ExpectedTotalChanges = 2;

            var context = new SerializableMessage();
            var aggregate = new SerializableEventCentricAggregateRoot(context);
            var request = new SetRequest(context, Guid.NewGuid());
            var bus = new Mock<IBus>();
            var cloner = new BinaryFormatterCloner();
            var repository = new UnversionedMemoryRepository<SerializableEventCentricAggregateRoot>(cloner);
            var propagator = new DomainEventPropagator<SerializableEventCentricAggregateRoot>(bus.Object, repository);
            DomainEvent[] changes = Array.Empty<DomainEvent>();

            _ = bus
                .Setup(b => b.PublishAsync(It.IsAny<DomainEvent[]>()))
                .Callback<DomainEvent[]>(events => changes = events);

            aggregate.Set(request);

            await repository.SaveAsync(aggregate);

            _ = Assert.Single(changes.OfType<SerializableCreatedDomainEvent>());
            _ = Assert.Single(changes.OfType<SerializableSetDomainEvent>());
            Assert.Equal(ExpectedTotalChanges, changes.Length);
        }
    }
}