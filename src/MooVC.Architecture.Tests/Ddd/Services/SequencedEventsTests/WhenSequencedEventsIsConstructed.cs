namespace MooVC.Architecture.Ddd.Services.SequencedEventsTests
{
    using System;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenSequencedEventsIsConstructed
    {
        [Fact]
        public void GivenNoEventsThenAnArgumentExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(default));
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void GiveEventsFromASingleAggregateVersionThenAnInstanceIsCreated(ulong sequence)
        {
            const int ExpectedCount = 2;

            var version = new SerializableAggregateRoot().ToVersionedReference();
            var context = new SerializableMessage();
            var first = new SerializableDomainEvent(context, version);
            var second = new SerializableDomainEvent(context, version);

            var events = new SequencedEvents(sequence, first, second);

            Assert.Equal(ExpectedCount, events.Events.Count());
            Assert.Equal(sequence, events.Sequence);
            Assert.Contains(first, events.Events);
            Assert.Contains(second, events.Events);
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void GiveEventsFromASingleAggregateVersionButTwoDifferentContextsThenAnArgumentExceptionIsThrown(ulong sequence)
        {
            var version = new SerializableAggregateRoot().ToVersionedReference();
            var firstContext = new SerializableMessage();
            var secondContext = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent(firstContext, version);
            var secondEvent = new SerializableDomainEvent(secondContext, version);

            _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void GiveEventsFromTwoAggregatesThenAnArgumentExceptionIsThrown(ulong sequence)
        {
            var firstVersion = new SerializableAggregateRoot().ToVersionedReference();
            var secondVersion = new SerializableAggregateRoot().ToVersionedReference();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent(context, firstVersion);
            var secondEvent = new SerializableDomainEvent(context, secondVersion);

            _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void GiveEventsFromTwoDifferentVersionsOfTheSameAggregateThenAnArgumentExceptionIsThrown(ulong sequence)
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var firstVersion = aggregate.ToVersionedReference();
            var context = new SerializableMessage();

            aggregate.MarkChangesAsCommitted();
            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            var secondVersion = aggregate.ToVersionedReference();
            var firstEvent = new SerializableDomainEvent(context, firstVersion);
            var secondEvent = new SerializableDomainEvent(context, secondVersion);

            _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
        }
    }
}