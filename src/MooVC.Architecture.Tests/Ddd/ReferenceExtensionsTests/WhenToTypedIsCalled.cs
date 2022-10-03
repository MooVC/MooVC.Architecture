namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests;

using System;
using Xunit;

public sealed class WhenToTypedIsCalled
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenAMatchingReferenceThenNoExceptionIsThrown(bool unversioned)
    {
        var aggregate = new SerializableAggregateRoot();
        Reference generic = aggregate.ToReference();
        Reference<SerializableAggregateRoot> typed = generic.ToTyped<SerializableAggregateRoot>(unversioned: unversioned);

        Assert.Equal(generic, typed);
        Assert.NotEqual(unversioned, typed.IsVersioned);
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