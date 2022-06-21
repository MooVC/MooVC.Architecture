namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using System.Collections.Generic;
using System.Linq;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenLoadFromHistoryIsCalled
{
    [Fact]
    public void GivenEventsThatOccurAfterTheCurrentSequeneThenAnAggregateHistoryInvalidForStateExceptionIsThrown()
    {
        var first = new SerializableEventCentricAggregateRoot();
        var second = new SerializableEventCentricAggregateRoot(first.Id);
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = first.ApplyChanges(context, times: 5);

        second.LoadFromHistory(events.Take(2));

        AggregateHistoryInvalidForStateException exception = Assert.Throws<AggregateHistoryInvalidForStateException>(
            () => second.LoadFromHistory(events.Skip(3)));

        Assert.True(exception.Aggregate.IsMatch(second));
    }

    [Fact]
    public void GivenEventsForADifferentAggregateIdThenAnAggregateEventMismatchExceptionIsThrown()
    {
        var first = new SerializableEventCentricAggregateRoot();
        var second = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = first.ApplyChanges(context, times: 1);

        AggregateEventMismatchException exception = Assert.Throws<AggregateEventMismatchException>(
            () => second.LoadFromHistory(events));

        Assert.True(exception.Aggregate.IsMatch(second));
    }

    [Fact]
    public void GivenAnAggregateThatHasChangesThenAnAggregateHasUncommittedChangesExceptionIsThrown()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = aggregate.ApplyChanges(context, commit: false, times: 1);

        AggregateHasUncommittedChangesException exception = Assert.Throws<AggregateHasUncommittedChangesException>(
            () => aggregate.LoadFromHistory(events));

        Assert.True(exception.Aggregate.IsMatch(aggregate));
    }

    [Fact]
    public void GivenUnorderedEventsThenAnAggregateEventSequenceUnorderedExceptionIsThrown()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = aggregate.ApplyChanges(context, times: 3);

        AggregateEventSequenceUnorderedException exception = Assert.Throws<AggregateEventSequenceUnorderedException>(
            () => aggregate.LoadFromHistory(events.OrderByDescending(@event => @event.Aggregate.Version)));

        Assert.True(exception.Aggregate.IsMatch(aggregate));
    }

    [Fact]
    public void GivenEventsStartingFromTheBeginningThenTheHydratedAggregateMatchesTheOriginal()
    {
        const ulong ExpectedVersionNumer = 1;

        var original = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = original.ApplyChanges(context, times: 1);

        var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

        hydrated.LoadFromHistory(events);

        Assert.Equal(original, hydrated);
        Assert.Equal(original.Value, hydrated.Value);
        Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
    }

    [Fact]
    public void GivenEventsStartingFromTheBeginningContainingMultipleVersionsThenTheHydratedAggregateMatchesTheOriginal()
    {
        const ulong ExpectedVersionNumer = 3;

        var original = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        IEnumerable<DomainEvent> events = original.ApplyChanges(context);

        var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

        hydrated.LoadFromHistory(events);

        Assert.Equal(original, hydrated);
        Assert.Equal(original.Value, hydrated.Value);
        Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
    }

    [Fact]
    public void GivenEventsStartingFromAPreviouslyCommittedVersionThenTheHydratedAggregateMatchesTheOriginal()
    {
        const ulong ExpectedVersionNumer = 4;

        var original = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        _ = original.ApplyChanges(context, times: 1);

        SerializableEventCentricAggregateRoot hydrated = original.Clone();

        IEnumerable<DomainEvent> events = original.ApplyChanges(context);

        hydrated.LoadFromHistory(events);

        Assert.Equal(original, hydrated);
        Assert.Equal(original.Value, hydrated.Value);
        Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
    }
}