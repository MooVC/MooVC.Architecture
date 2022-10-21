namespace MooVC.Architecture.Ddd.Services.AggregateAbortedSavingAsyncEventArgsTests;

using System;
using Xunit;

public sealed class WhenAggregateSavingAbortedAsyncEventArgsIsConstructed
{
    [Fact]
    public void GivenAnAggregateThenAnInstanceIsCreated()
    {
        var aggregate = new SerializableAggregateRoot();
        var reason = new InvalidDataException();
        var @event = new AggregateSavingAbortedAsyncEventArgs<SerializableAggregateRoot>(aggregate, reason);

        Assert.Equal(aggregate, @event.Aggregate);
        Assert.Same(aggregate, @event.Aggregate);
    }

    [Fact]
    public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
    {
        SerializableAggregateRoot? aggregate = default;
        var reason = new InvalidDataException();

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateSavingAbortedAsyncEventArgs<SerializableAggregateRoot>(aggregate!, reason));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateAndANullReasonThenAnArgumentNullExceptionIsThrown()
    {
        var aggregate = new SerializableAggregateRoot();
        InvalidDataException? reason = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateSavingAbortedAsyncEventArgs<SerializableAggregateRoot>(aggregate, reason!));

        Assert.Equal(nameof(reason), exception.ParamName);
    }
}