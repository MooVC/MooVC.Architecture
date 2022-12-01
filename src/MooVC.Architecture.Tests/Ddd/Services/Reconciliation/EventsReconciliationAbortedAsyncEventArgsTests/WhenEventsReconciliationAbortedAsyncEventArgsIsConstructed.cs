namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventsReconciliationAbortedAsyncEventArgsTests;

using System;
using System.Collections.Generic;
using System.Linq;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenEventsReconciliationAbortedAsyncEventArgsIsConstructed
{
    [Fact]
    public void GivenEventsThenAnInstanceIsReturnedWithTheEventsSet()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(aggregate, context) };
        var reason = new InvalidOperationException();
        var @event = new EventsReconciliationAbortedAsyncEventArgs(events, reason);

        Assert.Equal(events, @event.Events);
    }

    [Fact]
    public void GivenEmptyEventsThenAnArgumentExceptionIsThrown()
    {
        IEnumerable<SerializableCreatedDomainEvent> events = Enumerable.Empty<SerializableCreatedDomainEvent>();
        var reason = new InvalidOperationException();

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new EventsReconciliationAbortedAsyncEventArgs(events, reason));

        Assert.Equal(nameof(events), exception.ParamName);
    }

    [Fact]
    public void GivenNullEventsThenAnArgumentNullExceptionIsThrown()
    {
        IEnumerable<SerializableCreatedDomainEvent>? events = default;
        var reason = new InvalidOperationException();

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new EventsReconciliationAbortedAsyncEventArgs(events!, reason));

        Assert.Equal(nameof(events), exception.ParamName);
    }

    [Fact]
    public void GivenEventsAndNoReasonThenAnArgumentNullExceptionIsThrown()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(aggregate, context) };
        InvalidOperationException? reason = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new EventsReconciliationAbortedAsyncEventArgs(events, reason!));

        Assert.Equal(nameof(reason), exception.ParamName);
    }
}