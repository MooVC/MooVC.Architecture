namespace MooVC.Architecture.Ddd.DomainEventTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenDomainEventIsConstructed
    {
        [Fact]
        public void GivenAContextAndAnAggregateThenTheContextAndAggregateReferenceArePropagated()
        {
            var aggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();

            var @event = new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate);

            Assert.True(@event.Aggregate.IsMatch(aggregate));
            Assert.Equal(context.Id, @event.CausationId);
            Assert.Equal(context.CorrelationId, @event.CorrelationId);
        }

        [Fact]
        public void GivenAContextAndNoAggregateThenAnArgumentNullExceptionIsThrown()
        {
            var context = new SerializableMessage();

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent<SerializableAggregateRoot>(context, default!));
        }

        [Fact]
        public void GivenNoContextAndAnAggregateThenAnArgumentNullExceptionIsThrown()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableDomainEvent<SerializableAggregateRoot>(default!, aggregate));
        }
    }
}