namespace MooVC.Architecture.Ddd.GuidExtensionsTests;

using System;
using Xunit;

public sealed class WhenToReferenceIsCalled
{
    [Fact]
    public void GivenAnEmptyGuidThenTheEmptyReferenceIsReturned()
    {
        var reference = Guid.Empty.ToReference<SerializableAggregateRoot>();

        Assert.Same(Reference<SerializableAggregateRoot>.Empty, reference);
    }

    [Fact]
    public void GivenANonEmptyGuidThenAReferenceWithThatIdIsReturned()
    {
        var id = Guid.NewGuid();
        var reference = id.ToReference<SerializableAggregateRoot>();

        Assert.NotEqual(Reference<SerializableAggregateRoot>.Empty, reference);
        Assert.Equal(id, reference.Id);
    }
}