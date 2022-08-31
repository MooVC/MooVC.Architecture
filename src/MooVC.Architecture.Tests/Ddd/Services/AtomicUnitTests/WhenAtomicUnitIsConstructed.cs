namespace MooVC.Architecture.Ddd.Services.AtomicUnitTests;

using System;
using System.Linq;
using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.Ddd.Services;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenAtomicUnitIsConstructed
{
    [Fact]
    public void GivenANullEventThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(default(DomainEvent)!));
    }

    [Fact]
    public void GivenNullEventsThenAnArgumentNullExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentNullException>(() => new AtomicUnit(default(DomainEvent[])!));
    }

    [Fact]
    public void GivenNoEventsThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(Array.Empty<DomainEvent>()));
    }

    [Fact]
    public void GiveEventsFromASingleAggregateVersionThenAnInstanceIsCreated()
    {
        const int ExpectedCount = 2;

        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var first = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);
        var second = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);

        var unit = new AtomicUnit(new[] { first, second });

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
        var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, firstContext);
        var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, secondContext);

        _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(new[] { firstEvent, secondEvent }));
    }

    [Fact]
    public void GiveEventsFromTwoAggregatesThenAnArgumentExceptionIsThrown()
    {
        var firstAggregate = new SerializableAggregateRoot();
        var secondAggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(firstAggregate, context);
        var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(secondAggregate, context);

        _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(new[] { firstEvent, secondEvent }));
    }

    [Fact]
    public void GiveEventsFromTwoDifferentVersionsOfTheSameAggregateThenAnArgumentExceptionIsThrown()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(aggregate, context);

        aggregate.MarkChangesAsCommitted();
        aggregate.Set(new SetRequest(context, Guid.NewGuid()));

        var secondEvent = new SerializableDomainEvent<SerializableEventCentricAggregateRoot>(aggregate, context);

        _ = Assert.Throws<ArgumentException>(() => new AtomicUnit(new[] { firstEvent, secondEvent }));
    }
}