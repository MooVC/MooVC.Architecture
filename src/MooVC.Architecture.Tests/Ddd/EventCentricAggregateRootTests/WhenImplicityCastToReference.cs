namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using Xunit;

public sealed class WhenImplicityCastToReference
{
    [Fact]
    public void GivenAnAggregateThenAReferenceForThatAggregateIsReturned()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        Reference reference = aggregate;

        Assert.True(reference.IsMatch(aggregate));
    }

    [Fact]
    public void GivenANullAggregateThenAnEmptyReferenceIsReturned()
    {
        SerializableEventCentricAggregateRoot? aggregate = default;
        Reference reference = aggregate;

        Assert.True(reference.IsEmpty);
    }
}