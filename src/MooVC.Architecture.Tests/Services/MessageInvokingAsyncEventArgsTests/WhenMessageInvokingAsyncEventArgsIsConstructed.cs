namespace MooVC.Architecture.Services.MessageInvokingAsyncEventArgsTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenMessageInvokingAsyncEventArgsIsConstructed
{
    [Fact]
    public void GivenAnAggregateThenAnInstanceIsCreated()
    {
        var message = new SerializableMessage();
        var @event = new MessageInvokingAsyncEventArgs(message);

        Assert.Equal(message, @event.Message);
        Assert.Same(message, @event.Message);
    }

    [Fact]
    public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
    {
        SerializableMessage? message = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new MessageInvokingAsyncEventArgs(message!));

        Assert.Equal(nameof(message), exception.ParamName);
    }
}