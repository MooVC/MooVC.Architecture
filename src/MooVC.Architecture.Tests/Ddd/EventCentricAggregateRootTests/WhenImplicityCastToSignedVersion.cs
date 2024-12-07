namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using Xunit;

public sealed class WhenImplicityCastToSignedVersion
{
    [Fact]
    public void GivenAnAggregateThenTheVersionOfThatAggregateIsReturned()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        Sequence version = aggregate;

        Assert.Equal(aggregate.Version, version);
    }

    [Fact]
    public void GivenNoAggregateThenAnEmptyVersionIsReturned()
    {
        SerializableEventCentricAggregateRoot? aggregate = default;
        Sequence version = aggregate;

        Assert.Equal(Sequence.Empty, version);
    }
}