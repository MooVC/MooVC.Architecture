namespace MooVC.Architecture.Ddd.MessageExtensionsTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenTryIdentifyIsCalled
{
    [Fact]
    public void GivenAMessageThenAnEmptyReferenceIsReturned()
    {
        var message = new SerializableMessage();
        bool wasIdentified = message.TryIdentify(out Reference<SerializableAggregateRoot> aggregate);

        Assert.False(wasIdentified);
        Assert.NotNull(aggregate);
        Assert.True(aggregate.IsEmpty);
    }

    [Fact]
    public void GivenAContextualMessageThenTheContextualReferenceIsReturned()
    {
        var expected = Guid
            .NewGuid()
            .ToReference<SerializableAggregateRoot>();

        var message = new ContextualMessage(expected);
        bool wasIdentified = message.TryIdentify(out Reference<SerializableAggregateRoot> actual);

        Assert.True(wasIdentified);
        Assert.NotNull(actual);
        Assert.False(actual.IsEmpty);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAContextualEventThenTheContextualReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var message = new ContextualEvent(aggregate, context);
        bool wasIdentified = message.TryIdentify(out Reference<SerializableAggregateRoot> actual);

        Assert.True(wasIdentified);
        Assert.NotNull(actual);
        Assert.False(actual.IsEmpty);
        Assert.Equal(message.Aggregate, actual);
    }
}