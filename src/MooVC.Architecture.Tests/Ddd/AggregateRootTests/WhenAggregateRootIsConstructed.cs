namespace MooVC.Architecture.Ddd.AggregateRootTests;

using System;
using Xunit;

public sealed class WhenAggregateRootIsConstructed
{
    [Fact]
    public void GivenAnEmptyIdThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new SerializableAggregateRoot(Guid.Empty));
    }

    [Fact]
    public void GivenNoIdThenAnInstanceIsCreated()
    {
        var aggregate = new SerializableAggregateRoot();

        Assert.True(aggregate.HasUncommittedChanges);
    }

    [Fact]
    public void GivenAnIdThenTheIdIsPropagated()
    {
        var expectedId = Guid.NewGuid();
        var aggregate = new SerializableAggregateRoot(expectedId);

        Assert.Equal(expectedId, aggregate.Id);
        Assert.True(aggregate.HasUncommittedChanges);
    }
}