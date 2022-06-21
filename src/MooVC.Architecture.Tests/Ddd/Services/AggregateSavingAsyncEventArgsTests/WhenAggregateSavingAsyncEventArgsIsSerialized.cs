namespace MooVC.Architecture.Ddd.Services.AggregateSavingAsyncEventArgsTests;

using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateSavingAsyncEventArgsIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableAggregateRoot();
        var @event = new AggregateSavingAsyncEventArgs<SerializableAggregateRoot>(aggregate);

        AggregateSavingAsyncEventArgs<SerializableAggregateRoot> deserialized = @event.Clone();

        Assert.Equal(@event.Aggregate, deserialized.Aggregate);
        Assert.NotSame(@event.Aggregate, deserialized.Aggregate);
        Assert.NotSame(@event, deserialized);
    }
}