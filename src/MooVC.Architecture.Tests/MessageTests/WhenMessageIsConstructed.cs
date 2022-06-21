namespace MooVC.Architecture.MessageTests;

using System;
using Xunit;

public sealed class WhenMessageIsConstructed
{
    [Fact]
    public void GivenNoValuesThenCausationIsSetToEmptyAndANewCorrelationIsGenerated()
    {
        var message = new SerializableMessage();

        Assert.Equal(Guid.Empty, message.CausationId);
        Assert.NotEqual(Guid.Empty, message.CorrelationId);
    }

    [Fact]
    public void GivenAnInstanceBasedOnAnInstanceThenTheCorrelationPropagatedAndTheCausationIsSetToTheIdOfTheOriginal()
    {
        var expected = new SerializableMessage();
        var message = new SerializableMessage(context: expected);

        Assert.Equal(expected.Id, message.CausationId);
        Assert.Equal(expected.CorrelationId, message.CorrelationId);
    }
}