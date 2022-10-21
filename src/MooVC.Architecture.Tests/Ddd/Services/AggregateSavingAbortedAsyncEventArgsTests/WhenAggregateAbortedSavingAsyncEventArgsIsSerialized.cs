namespace MooVC.Architecture.Ddd.Services.AggregateSavingAbortedAsyncEventArgsTests;

using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateAbortedSavingAsyncEventArgsIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableAggregateRoot();
        var reason = new InvalidDataException();
        var @event = new AggregateSavingAbortedAsyncEventArgs<SerializableAggregateRoot>(aggregate, reason);

        AggregateSavingAbortedAsyncEventArgs<SerializableAggregateRoot> deserialized = @event.Clone();

        Assert.Equal(@event.Aggregate, deserialized.Aggregate);
        Assert.NotSame(@event.Aggregate, deserialized.Aggregate);
        Assert.NotSame(@event, deserialized);
    }
}