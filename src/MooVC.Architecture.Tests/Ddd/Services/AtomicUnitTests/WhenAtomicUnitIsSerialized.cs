﻿namespace MooVC.Architecture.Ddd.Services.AtomicUnitTests;

using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAtomicUnitIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();

        SerializableDomainEvent<SerializableAggregateRoot>[] events = new[]
        {
            new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context),
        };

        var original = new AtomicUnit(events);
        AtomicUnit deserialized = original.Clone();

        Assert.Equal(original.Id, deserialized.Id);
        Assert.Equal(original.Events, deserialized.Events);
        Assert.NotSame(original, deserialized);
    }
}