namespace MooVC.Architecture.Ddd.Specifications.EnsureTests;

using System;
using Xunit;
using static MooVC.Architecture.Ddd.Specifications.Ensure;
using static MooVC.Architecture.Ddd.Specifications.EnsureTests.Resources;

public sealed class WhenSatisfiesIsCalled
{
    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAValueTypeWhenTheSpecificationPassesThenTheValueIsReturned(string? argumentName, string? message)
    {
        const int Expected = 5;

        int actual = Assertion(Expected, argumentName, message, new PassingValueSpecification());

        Assert.Equal(Expected, actual);
    }

    [Theory]
    [InlineData(default, 4, default)]
    [InlineData(default, 3, "Expected Message")]
    [InlineData("Expected Name", 2, default)]
    [InlineData("The Name", 1, "The Message")]
    public void GivenAValueTypeAndADefaultWhenTheSpecificationFailsThenTheDefaultIsReturned(string? argumentName, int @default, string? message)
    {
        const int Value = 5;

        int actual = Assertion(Value, argumentName, message, new FailingValueSpecification(), @default: @default);

        Assert.Equal(@default, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAValueTypeWhenTheSpecificationFailsThenAnArgumentExceptionIsThrown(string? argumentName, string? message)
    {
        const int Value = 5;

        ArgumentException exception = AssertThrows(Value, argumentName, message, new FailingValueSpecification());

        Assert.Contains(FailingValueSpecification.Requirement, exception.Message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAValueTypeWhenTheSpecificationFailsWithAnEmbeddedMessageThenAnArgumentExceptionIsThrownWithTheMessage(
        string? argumentName,
        string? message)
    {
        const int Value = 5;

        ArgumentException exception = AssertThrows(Value, argumentName, message, new EmbeddedFailingValueSpecification());

        Assert.Contains(EmbeddedFailingValueSpecificationRequirement, exception.Message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAValueTypeWhenTheSpecificationFailsWithAnIncorrectEmbeddedMessageThenAnArgumentExceptionIsThrown(
        string? argumentName,
        string? message)
    {
        const int Value = 5;

        _ = AssertThrows(Value, argumentName, message, new IncorrectEmbeddedFailingValueSpecification());
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAReferenceTypeWhenTheSpecificationPassesThenTheReferenceIsReturned(string? argumentName, string? message)
    {
        const string Expected = "Irrelevant value";

        string actual = Assertion(Expected, argumentName, message, new PassingReferenceSpecification());

        Assert.Equal(Expected, actual);
    }

    [Theory]
    [InlineData(default, "Something", default)]
    [InlineData(default, "something", "Expected Message")]
    [InlineData("Expected Name", "dark", default)]
    [InlineData("The Name", "side", "The Message")]
    public void GivenAReferenceTypeAndADefaultWhenTheSpecificationFailsThenTheDefaultIsReturned(
        string? argumentName,
        string @default,
        string? message)
    {
        const string Expected = "Irrelevant value";

        string actual = Assertion(Expected, argumentName, message, new FailingReferenceSpecification(), @default: @default);

        Assert.Equal(@default, actual);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAReferenceTypeWhenTheSpecificationFailsThenAnArgumentExceptionIsThrown(string? argumentName, string? message)
    {
        const string Expected = "Irrelevant value";

        ArgumentException exception = AssertThrows(Expected, argumentName, message, new FailingReferenceSpecification());

        Assert.Contains(FailingReferenceSpecification.Requirement, exception.Message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAReferenceTypeWhenTheSpecificationFailsWithAnEmbeddedMessageThenAnArgumentExceptionIsThrownWithTheMessage(
        string? argumentName,
        string? message)
    {
        const string Expected = "Irrelevant value";

        ArgumentException exception = AssertThrows(Expected, argumentName, message, new EmbeddedFailingReferenceSpecification());

        Assert.Contains(EmbeddedFailingReferenceSpecificationRequirement, exception.Message);
    }

    [Theory]
    [InlineData(default, default)]
    [InlineData(default, "Expected Message")]
    [InlineData("Expected Name", default)]
    [InlineData("The Name", "The Message")]
    public void GivenAReferenceTypeWhenTheSpecificationFailsWithAnIncorrectEmbeddedMessageThenAnArgumentExceptionIsThrown(
        string? argumentName,
        string? message)
    {
        const string Expected = "Irrelevant value";

        _ = AssertThrows(Expected, argumentName, message, new IncorrectEmbeddedFailingReferenceSpecification());
    }

    private static T Assertion<T>(T? argument, string? argumentName, string? message, Specification<T> specification, T? @default = default)
        where T : struct
    {
        if (argumentName is not null)
        {
            return Satisifies(argument, specification, argumentName: argumentName, @default: @default, message: message);
        }

        return Satisifies(argument, specification, @default: @default, message: message);
    }

    private static ArgumentException AssertThrows<T>(T? argument, string? argumentName, string? message, Specification<T> specification)
        where T : struct
    {
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() =>
            Assertion(argument, argumentName, message, specification));

        argumentName ??= nameof(argument);

        Assert.Equal(argumentName, exception.ParamName);

        if (message is not null)
        {
            Assert.StartsWith(message, exception.Message);
        }

        return exception;
    }

    private static T Assertion<T>(T? argument, string? argumentName, string? message, Specification<T> specification, T? @default = default)
        where T : class
    {
        if (argumentName is not null)
        {
            return Satisifies(argument, specification, argumentName: argumentName, @default: @default, message: message);
        }

        return Satisifies(argument, specification, @default: @default, message: message);
    }

    private static ArgumentException AssertThrows<T>(T? argument, string? argumentName, string? message, Specification<T> specification)
        where T : class
    {
        ArgumentException exception = Assert.ThrowsAny<ArgumentException>(() =>
            Assertion(argument, argumentName, message, specification));

        argumentName ??= nameof(argument);

        Assert.Equal(argumentName, exception.ParamName);

        if (message is not null)
        {
            Assert.StartsWith(message, exception.Message);
        }

        return exception;
    }
}