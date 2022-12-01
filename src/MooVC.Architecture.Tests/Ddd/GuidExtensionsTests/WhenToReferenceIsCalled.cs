namespace MooVC.Architecture.Ddd.GuidExtensionsTests;

using System;
using Xunit;

public sealed class WhenToReferenceIsCalled
{
    [Fact]
    public void GivenAnEmptyGuidWhenAnUntypedReferenceIsRequestedThenTheEmptyReferenceIsReturned()
    {
        var reference = Guid.Empty.ToReference(typeof(SerializableAggregateRoot));

        Assert.Same(Reference<SerializableAggregateRoot>.Empty, reference);
    }

    [Fact]
    public void GivenANonEmptyGuidWhenAnUntypedReferenceIsRequestedThenAReferenceWithThatIdIsReturned()
    {
        var id = Guid.NewGuid();
        var reference = id.ToReference(typeof(SerializableAggregateRoot));

        Assert.NotEqual(Reference<SerializableAggregateRoot>.Empty, reference);
        Assert.Equal(id, reference.Id);
    }

    [Fact]
    public void GivenAnEmptyGuidWhenAnTypedReferenceIsRequestedThenTheEmptyReferenceIsReturned()
    {
        var reference = Guid.Empty.ToReference<SerializableAggregateRoot>();

        Assert.Same(Reference<SerializableAggregateRoot>.Empty, reference);
    }

    [Fact]
    public void GivenANonEmptyGuidWhenAnTypedReferenceIsRequestedThenAReferenceWithThatIdIsReturned()
    {
        var id = Guid.NewGuid();
        var reference = id.ToReference<SerializableAggregateRoot>();

        Assert.NotEqual(Reference<SerializableAggregateRoot>.Empty, reference);
        Assert.Equal(id, reference.Id);
    }
}