namespace MooVC.Architecture.Ddd.EnsureTests;

using System;
using MooVC.Architecture.Ddd;
using Xunit;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Reference;

public sealed class WhenReferenceIsNotEmptyIsCalled
{
    [Fact]
    public void GivenAnEmptyReferenceThenAnArgumentExceptionIsThrown()
    {
        Reference reference = Reference<SerializableAggregateRoot>.Empty;

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => ReferenceIsNotEmpty(reference, nameof(reference)));

        Assert.Equal(nameof(reference), exception.ParamName);
    }

    [Fact]
    public void GivenAnEmptyReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
    {
        Reference reference = Reference<SerializableAggregateRoot>.Empty;
        string message = "Some sessage";

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => ReferenceIsNotEmpty(reference, nameof(reference), message));

        Assert.StartsWith(message, exception.Message);
    }

    [Fact]
    public void GivenANonEmptyReferenceThenNoExceptionIsThrown()
    {
        Reference reference = Create<SerializableAggregateRoot>(Guid.NewGuid());

        _ = ReferenceIsNotEmpty(reference, nameof(reference));
    }
}