namespace MooVC.Architecture.Ddd.AggregateRootTests;

using System;
using Xunit;

public sealed class WhenImplicityCastToGuid
{
    [Fact]
    public void GivenAnAggregateThenTheIdForThatAggregateIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        Guid id = aggregate;

        Assert.Equal(aggregate.Id, id);
    }

    [Fact]
    public void GivenANullAggregateThenAnEmptyIdIsReturned()
    {
        SerializableAggregateRoot? aggregate = default;
        Guid id = aggregate;

        Assert.Equal(Guid.Empty, id);
    }
}