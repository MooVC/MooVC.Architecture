﻿namespace MooVC.Architecture.Ddd.DomainExceptionTests;

using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenDomainExceptionIsConstructed
{
    [Fact]
    public void GivenAnAggregateAContextAndAMessageInstanceIsReturnedWithAllPropertiesPropagated()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        const string Message = "Something something dark side.";

        var exception = new SerializableDomainException<SerializableEventCentricAggregateRoot>(aggregate, context, Message);

        Assert.Equal(aggregate.ToReference(), exception.Aggregate);
        Assert.Equal(context, exception.Context);
        Assert.Equal(Message, exception.Message);
    }

    [Fact]
    public void GivenAnAggregateReferenceAContextAndAMessageInstanceIsReturnedWithAllPropertiesPropagated()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var reference = aggregate.ToReference();
        var context = new SerializableMessage();
        const string Message = "Something something dark side.";

        var exception = new SerializableDomainException<SerializableEventCentricAggregateRoot>(reference, context, Message);

        Assert.Equal(reference, exception.Aggregate);
        Assert.Equal(context, exception.Context);
        Assert.Equal(Message, exception.Message);
    }
}