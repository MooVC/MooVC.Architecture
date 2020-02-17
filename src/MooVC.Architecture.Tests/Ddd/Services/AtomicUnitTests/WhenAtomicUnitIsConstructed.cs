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

            var version = new SerializableAggregateRoot().ToVersionedReference();
            var context = new SerializableMessage();
            var first = new SerializableDomainEvent(context, version);
            var second = new SerializableDomainEvent(context, version);

            var unit = new AtomicUnit(first, second);

            Assert.Equal(ExpectedCount, unit.Events.Count());
            Assert.Contains(first, unit.Events);
            Assert.Contains(second, unit.Events);
        }

        [Fact]
        public void GiveEventsFromTwoAggregatesThenAnArgumentExceptionIsThrown()
        {
            var firstVersion = new SerializableAggregateRoot().ToVersionedReference();
            var secondVersion = new SerializableAggregateRoot().ToVersionedReference();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent(context, firstVersion);
            var secondEvent = new SerializableDomainEvent(context, secondVersion);

            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(firstEvent, secondEvent));
        }

        [Fact]
        public void GiveEventsFromTwoDifferentVersionsOfTheSameAggregateThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var firstVersion = aggregate.ToVersionedReference();
            var context = new SerializableMessage();

            aggregate.MarkChangesAsCommitted();
            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            var secondVersion = aggregate.ToVersionedReference();
            var firstEvent = new SerializableDomainEvent(context, firstVersion);
            var secondEvent = new SerializableDomainEvent(context, secondVersion);

            _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(firstEvent, secondEvent));
        }
    }
}