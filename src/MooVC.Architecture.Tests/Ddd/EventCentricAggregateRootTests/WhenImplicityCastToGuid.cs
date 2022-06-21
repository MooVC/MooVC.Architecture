namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using System;
using Xunit;

public sealed class WhenImplicityCastToGuid
{
    [Fact]
    public void GivenAnAggregateThenTheIdForThatAggregateIsReturned()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        Guid id = aggregate;

        Assert.Equal(aggregate.Id, id);
    }

    [Fact]
    public void GivenANullAggregateThenAnEmptyIdIsReturned()
    {
        SerializableEventCentricAggregateRoot? aggregate = default;
        Guid id = aggregate;

        Assert.Equal(Guid.Empty, id);
    }
}