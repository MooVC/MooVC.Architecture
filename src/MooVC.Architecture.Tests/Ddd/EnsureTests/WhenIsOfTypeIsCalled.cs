namespace MooVC.Architecture.Ddd.EnsureTests;

using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using MooVC.Architecture.Ddd;
using Xunit;
using static MooVC.Architecture.Ddd.Ensure;

public sealed class WhenIsOfTypeIsCalled
{
    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMatchingReferenceThenNoExceptionIsThrown(string? argumentName, string? message)
    {
        Reference expected = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

        Reference<SerializableAggregateRoot> actual = Assertion<SerializableAggregateRoot>(expected, argumentName, message);

        Assert.Same(expected, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown(string? argumentName, string? message)
    {
        Reference expected = Reference<SerializableAggregateRoot>.Empty;

        Reference<SerializableAggregateRoot> actual = Assertion<SerializableAggregateRoot>(expected, argumentName, message);

        Assert.Same(expected, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown(string? argumentName, string? message)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());

        _ = AssertThrows<SerializableAggregateRoot>(reference, argumentName, message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown(string? argumentName, string? message)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;

        _ = AssertThrows<SerializableAggregateRoot>(reference, argumentName, message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMismatchingReferenceWhenADefaultIsProvidedThenTheDefaultIsReturned(string? argumentName, string? message)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());
        var @default = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

        Reference<SerializableAggregateRoot> actual = Assertion(reference, argumentName, message, @default: @default);

        Assert.Same(@default, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAMismatchingEmptyReferenceWhenADefaultIsProvidedThenTheDefaultIsReturned(string? argumentName, string? message)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;
        var @default = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

        Reference<SerializableAggregateRoot> actual = Assertion(reference, argumentName, message, @default: @default);

        Assert.Same(@default, actual);
    }

    private static Reference<TAggregate> Assertion<TAggregate>(
        Reference? argument,
        string? argumentName,
        string? message,
        Reference<TAggregate>? @default = default)
        where TAggregate : AggregateRoot
    {
        if (argumentName is { })
        {
            return IsOfType(argument, argumentName: argumentName, @default: @default, message: message);
        }

        return IsOfType(argument, @default: @default, message: message);
    }

    private static ArgumentException AssertThrows<TAggregate>(Reference? argument, string? argumentName, string? message)
        where TAggregate : AggregateRoot
    {
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() =>
            Assertion<TAggregate>(argument, argumentName, message));

        argumentName ??= nameof(argument);

        Assert.Equal(argumentName, exception.ParamName);

        if (message is { })
        {
            Assert.StartsWith(message, exception.Message);
        }

        return exception;
    }
}