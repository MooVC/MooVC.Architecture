﻿namespace MooVC.Architecture.Services.MessageInvokedAsyncEventArgsTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenMessageInvokedAsyncEventArgsIsConstructed
{
    [Fact]
    public void GivenAnAggregateThenAnInstanceIsCreated()
    {
        var message = new SerializableMessage();
        var @event = new MessageInvokedAsyncEventArgs(message);

        Assert.Equal(message, @event.Message);
        Assert.Same(message, @event.Message);
    }

    [Fact]
    public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
    {
        SerializableMessage? message = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new MessageInvokedAsyncEventArgs(message!));

        Assert.Equal(nameof(message), exception.ParamName);
    }
}