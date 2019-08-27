namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenDomainEventIsConstructed
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(18446744073709551615)]
        public void GivenAnContextAggregateReferenceAndAVersionThenTheContextAggregateAndVersionArePropagated(ulong expectedVersion)
        {
            var expectedId = Guid.NewGuid();
            var expectedAggregate = new Reference<AggregateRoot>(expectedId);
            var expectedContext = new SerializableMessage();

            var @event = new SerializableDomainEvent(expectedContext, expectedAggregate, expectedVersion);
            
            Assert.Equal(expectedAggregate, @event.Aggregate);
            Assert.Equal(expectedContext.Id, @event.CausationId);
            Assert.Equal(expectedContext.CorrelationId, @event.CorrelationId);
            Assert.Equal(expectedVersion, @event.Version);
        }

        [Fact]
        public void GivenAContextNoAggregateReferenceAndAVersionThenAnArgumentNullExceptionIsThrown()
        {
            var id = Guid.NewGuid();
            var context = new SerializableMessage();

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent(context, null, AggregateRoot.DefaultVersion));
        }

        [Fact]
        public void GivenNoContextAnAggregateReferenceAndAVersionThenAnArgumentNullExceptionIsThrown()
        {
            var id = Guid.NewGuid();
            var aggregate = new Reference<AggregateRoot>(id);

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent(null, aggregate, AggregateRoot.DefaultVersion));
        }
    }
}