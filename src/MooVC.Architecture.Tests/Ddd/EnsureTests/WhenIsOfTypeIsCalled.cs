namespace MooVC.Architecture.Ddd.EnsureTests;

using System;
using MooVC.Architecture.Ddd;
using Xunit;
using static MooVC.Architecture.Ddd.Ensure;

public sealed class WhenIsOfTypeIsCalled
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
    public void GivenAMatchingReferenceThenNoExceptionIsThrown(string? argumentName, string? message, bool unversioned)
    {
        var aggregate = new SerializableAggregateRoot();
        Reference expected = aggregate.ToReference();

        Reference<SerializableAggregateRoot> actual = Assertion<SerializableAggregateRoot>(
            expected,
            argumentName,
            message,
            unversioned: unversioned);

        Assert.Equal(expected, actual);
        Assert.NotEqual(unversioned, actual.IsVersioned);
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
    public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown(string? argumentName, string? message, bool unversioned)
    {
        Reference expected = Reference<SerializableAggregateRoot>.Empty;

        Reference<SerializableAggregateRoot> actual = Assertion<SerializableAggregateRoot>(
            expected,
            argumentName,
            message,
            unversioned: unversioned);

        Assert.Equal(expected, actual);
        Assert.False(actual.IsVersioned);
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
    [InlineData(default, default, false)]
    [InlineData(default, "Expected Message", false)]
    [InlineData("Expected Name", default, false)]
    [InlineData("The Name", "The Message", false)]
    [InlineData(default, default, true)]
    [InlineData(default, "Expected Message", true)]
    [InlineData("Expected Name", default, true)]
    [InlineData("The Name", "The Message", true)]
    public void GivenAMismatchingReferenceWhenADefaultIsProvidedThenTheDefaultIsReturned(string? argumentName, string? message, bool unversioned)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());
        var @default = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

        Reference<SerializableAggregateRoot> actual = Assertion(reference, argumentName, message, @default: @default, unversioned: unversioned);

        Assert.Same(@default, actual);
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
    public void GivenAMismatchingEmptyReferenceWhenADefaultIsProvidedThenTheDefaultIsReturned(
        string? argumentName,
        string? message,
        bool unversioned)
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;
        var @default = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

        Reference<SerializableAggregateRoot> actual = Assertion(reference, argumentName, message, @default: @default, unversioned: unversioned);

        Assert.Same(@default, actual);
    }

    private static Reference<TAggregate> Assertion<TAggregate>(
        Reference? argument,
        string? argumentName,
        string? message,
        Reference<TAggregate>? @default = default,
        bool unversioned = false)
        where TAggregate : AggregateRoot
    {
        if (argumentName is not null)
        {
            return IsOfType(argument, argumentName: argumentName, @default: @default, message: message, unversioned: unversioned);
        }

        return IsOfType(argument, @default: @default, message: message, unversioned: unversioned);
    }

    private static ArgumentException AssertThrows<TAggregate>(Reference? argument, string? argumentName, string? message)
        where TAggregate : AggregateRoot
    {
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() =>
            Assertion<TAggregate>(argument, argumentName, message));

        argumentName ??= nameof(argument);

        Assert.Equal(argumentName, exception.ParamName);

        if (message is not null)
        {
            Assert.StartsWith(message, exception.Message);
        }

        return exception;
    }
}