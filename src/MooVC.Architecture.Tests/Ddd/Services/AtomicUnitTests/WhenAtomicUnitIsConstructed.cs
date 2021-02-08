namespace MooVC.Architecture.Ddd.Services.AtomicUnitTests
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenAtomicUnitIsConstructed
    {
        [Fact]
        public void GivenNoEventsThenAnArgumentExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit());
        }

        [Fact]
        public void GiveEventsFromASingleAggregateVersionThenAnInstanceIsCreated()
        {
            const int ExpectedCount = 2;

            var aggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();
            var first = new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate);
            var second = new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate);

            var unit = new AtomicUnit(first, second);

            Assert.Equal(ExpectedCount, unit.Events.Count());
            Assert.Contains(first, unit.Events);
            Assert.Contains(second, unit.Events);
        }

        [Fact]
        public void GiveEventsFromASingleAggregateVersionButTwoDifferentContextsThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableAggregateRoot();
            var firstContext = new SerializableMessage();
            var secondContext = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(firstContext, aggregate);
            var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(secondContext, aggregate);

            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(firstEvent, secondEvent));
        }

        [Fact]
        public void GiveEventsFromTwoAggregatesThenAnArgumentExceptionIsThrown()
        {
            var firstAggregate = new SerializableAggregateRoot();
            var secondAggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(context, firstAggregate);
            var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(context, secondAggregate);

            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(firstEvent, secondEvent));
        }

        [Fact]
        public void GiveEventsFromTwoDifferentVersionsOfTheSameAggregateThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(context, aggregate);

            aggregate.MarkChangesAsCommitted();
            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            var secondEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(context, aggregate);

            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(firstEvent, secondEvent));
        }
    }
}