namespace MooVC.Architecture.Ddd.Services.DomainEventPropagatorTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
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
            IEnumerable<DomainEvent> changes = Enumerable.Empty<DomainEvent>();

            _ = bus
                .Setup(b => b.PublishAsync(
                    It.IsAny<IEnumerable<DomainEvent>>(),
                    It.IsAny<CancellationToken?>()))
                .Callback<IEnumerable<DomainEvent>, CancellationToken?>((events, _) => changes = events);

            aggregate.Set(request);

            await repository.SaveAsync(aggregate);

            _ = Assert.Single(changes.OfType<SerializableCreatedDomainEvent>());
            _ = Assert.Single(changes.OfType<SerializableSetDomainEvent>());
            Assert.Equal(ExpectedTotalChanges, changes.Count());
        }
    }
}