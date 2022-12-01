namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventReconciliationAsyncEventArgsTests;

using System;
using System.Collections.Generic;
using System.Linq;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenEventReconciliationAsyncEventArgsIsConstructed
{
    [Fact]
    public void GivenEventsThenAnInstanceIsReturnedWithTheEventsSet()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(aggregate, context) };
        var @event = new EventsReconciliationAsyncEventArgs(events);

        Assert.Equal(events, @event.Events);
    }

    [Fact]
    public void GivenEmptyEventsThenAnArgumentExceptionIsThrown()
    {
        IEnumerable<SerializableCreatedDomainEvent> events = Enumerable.Empty<SerializableCreatedDomainEvent>();

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new EventsReconciliationAsyncEventArgs(events));

        Assert.Equal(nameof(events), exception.ParamName);
    }

    [Fact]
    public void GivenNullEventsThenAnArgumentNullExceptionIsThrown()
    {
        IEnumerable<SerializableCreatedDomainEvent>? events = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new EventsReconciliationAsyncEventArgs(events!));

        Assert.Equal(nameof(events), exception.ParamName);
    }
}