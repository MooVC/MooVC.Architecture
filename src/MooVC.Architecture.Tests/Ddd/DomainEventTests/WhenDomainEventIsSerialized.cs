namespace MooVC.Architecture.Ddd.AggregateRootTests;

using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenDomainEventIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableAggregateRoot();
        var expectedContext = new SerializableMessage();
        var original = new SerializableDomainEvent<SerializableAggregateRoot>(expectedContext, aggregate);
        SerializableDomainEvent<SerializableAggregateRoot> deserialized = original.Clone();

        Assert.Equal(original, deserialized);
        Assert.NotSame(original, deserialized);

        Assert.Equal(original.Aggregate, deserialized.Aggregate);
        Assert.Equal(original.CausationId, deserialized.CausationId);
        Assert.Equal(original.CorrelationId, deserialized.CorrelationId);
        Assert.Equal(original.Id, deserialized.Id);
        Assert.Equal(original.TimeStamp, deserialized.TimeStamp);
        Assert.Equal(original.GetHashCode(), deserialized.GetHashCode());
    }
}