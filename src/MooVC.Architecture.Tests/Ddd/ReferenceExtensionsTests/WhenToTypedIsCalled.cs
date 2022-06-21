namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests;

using System;
using Xunit;

public sealed class WhenToTypedIsCalled
{
    [Fact]
    public void GivenAMatchingReferenceThenNoExceptionIsThrown()
    {
        Reference generic = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());
        Reference<SerializableAggregateRoot> typed = generic.ToTyped<SerializableAggregateRoot>();

        Assert.Same(generic, typed);
    }

    [Fact]
    public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
    {
        Reference generic = Reference<SerializableAggregateRoot>.Empty;
        Reference<SerializableAggregateRoot> typed = generic.ToTyped<SerializableAggregateRoot>();

        Assert.Same(generic, typed);
    }

    [Fact]
    public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => reference.ToTyped<SerializableAggregateRoot>());

        Assert.Equal(nameof(reference), exception.ParamName);
    }

    [Fact]
    public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
    {
        Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => reference.ToTyped<SerializableAggregateRoot>());

        Assert.Equal(nameof(reference), exception.ParamName);
    }
}