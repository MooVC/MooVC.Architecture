namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenDomainEventIsConstructed
    {
        [Fact]
        public void GivenAContextAndAnAggregateReferenceThenTheContextAndAggregateReferenceArePropagated()
        {
            var aggregate = new SerializableAggregateRoot();
            var expectedAggregate = new VersionedReference<SerializableAggregateRoot>(aggregate);
            var expectedContext = new SerializableMessage();

            var @event = new SerializableDomainEvent(expectedContext, expectedAggregate);

            Assert.Equal(expectedAggregate, @event.Aggregate);
            Assert.Equal(expectedContext.Id, @event.CausationId);
            Assert.Equal(expectedContext.CorrelationId, @event.CorrelationId);
        }

        [Fact]
        public void GivenAContextAndNoAggregateReferenceThenAnArgumentNullExceptionIsThrown()
        {
            var context = new SerializableMessage();

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent(context, null));
        }

        [Fact]
        public void GivenNoContextAndAnAggregateReferenceThenAnArgumentNullExceptionIsThrown()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent(null, reference));
        }
    }
}