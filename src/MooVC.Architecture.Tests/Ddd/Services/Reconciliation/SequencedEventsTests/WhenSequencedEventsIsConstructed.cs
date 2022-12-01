namespace MooVC.Architecture.Ddd.Services.Reconciliation.SequencedEventsTests;

using System;
using System.Linq;
using MooVC.Architecture.Ddd.DomainEventTests;
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

        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var first = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);
        var second = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);

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
        var aggregate = new SerializableAggregateRoot();
        var firstContext = new SerializableMessage();
        var secondContext = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, firstContext);
        var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, secondContext);

        _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
    }

    [Theory]
    [InlineData(ulong.MinValue)]
    [InlineData(ulong.MaxValue)]
    public void GiveEventsFromTwoAggregatesThenAnArgumentExceptionIsThrown(ulong sequence)
    {
        var firstAggregate = new SerializableAggregateRoot();
        var secondAggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(firstAggregate, context);
        var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(secondAggregate, context);

        _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
    }

    [Theory]
    [InlineData(ulong.MinValue)]
    [InlineData(ulong.MaxValue)]
    public void GiveEventsFromTwoDifferentVersionsOfTheSameAggregateThenAnArgumentExceptionIsThrown(ulong sequence)
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(aggregate, context);

        aggregate.MarkChangesAsCommitted();
        aggregate.Set(new SetRequest(context, Guid.NewGuid()));

        var secondEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(aggregate, context);

        _ = Assert.Throws<ArgumentException>(() => new SequencedEvents(sequence, firstEvent, secondEvent));
    }
}