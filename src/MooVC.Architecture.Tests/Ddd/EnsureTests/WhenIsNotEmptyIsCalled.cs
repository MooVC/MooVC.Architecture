namespace MooVC.Architecture.Ddd.EnsureTests;

using System;
using MooVC.Architecture.Ddd;
using Xunit;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Reference;

public sealed class WhenIsNotEmptyIsCalled
{
    [Theory]
    [InlineData(default, default, false)]
    [InlineData(default, "Expected Message", false)]
    [InlineData("Expected Name", default, false)]
    [InlineData("The Name", "The Message", false)]
    [InlineData(default, default, true)]
    [InlineData(default, "Expected Message", true)]
    [InlineData("Expected Name", default, true)]
    [InlineData("The Name", "The Message", true)]
    public void GivenAnEmptyReferenceThenAnArgumentExceptionIsThrown(string? argumentName, string? message, bool isNull)
    {
        Reference? reference = isNull
            ? default
            : Reference<SerializableAggregateRoot>.Empty;

        _ = AssertThrows(reference, argumentName, message);
    }

    [Theory]
    [InlineData(default, default, false)]
    [InlineData(default, "Expected Message", false)]
    [InlineData("Expected Name", default, false)]
    [InlineData("The Name", "The Message", false)]
    [InlineData(default, default, true)]
    [InlineData(default, "Expected Message", true)]
    [InlineData("Expected Name", default, true)]
    [InlineData("The Name", "The Message", true)]
    public void GivenAnEmptyReferenceWhenADefaultIsProvidedThenTheDefaultIsReturned(string? argumentName, string? message, bool isNull)
    {
        Reference? reference = isNull
            ? default
            : Reference<SerializableAggregateRoot>.Empty;

        Reference @default = Create<SerializableAggregateRoot>(Guid.NewGuid());

        Reference actual = Assertion(reference, argumentName, message, @default: @default);

        Assert.Same(@default, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenANonEmptyReferenceThenNoExceptionIsThrown(string? argumentName, string? message)
    {
        Reference reference = Create<SerializableAggregateRoot>(Guid.NewGuid());

        Reference actual = Assertion(reference, argumentName, message);

        Assert.Same(reference, actual);
    }

    private static T Assertion<T>(T? argument, string? argumentName, string? message, T? @default = default)
        where T : Reference
    {
        if (argumentName is not null)
        {
            return IsNotEmpty(argument, argumentName: argumentName, @default: @default, message: message);
        }

        return IsNotEmpty(argument, @default: @default, message: message);
    }

    private static ArgumentException AssertThrows<T>(T? argument, string? argumentName, string? message)
        where T : Reference
    {
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() =>
            Assertion(argument, argumentName, message));

        argumentName ??= nameof(argument);

        Assert.Equal(argumentName, exception.ParamName);

        if (message is not null)
        {
            Assert.StartsWith(message, exception.Message);
        }

        return exception;
    }
}