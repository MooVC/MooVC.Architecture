namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using Xunit;

public sealed class WhenImplicityCastToSignedVersion
{
    [Fact]
    public void GivenAnAggregateThenTheVersionOfThatAggregateIsReturned()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        SignedVersion version = aggregate;

        Assert.Equal(aggregate.Version, version);
    }

    [Fact]
    public void GivenNoAggregateThenAnEmptyVersionIsReturned()
    {
        SerializableEventCentricAggregateRoot? aggregate = default;
        SignedVersion version = aggregate;

        Assert.Equal(SignedVersion.Empty, version);
    }
}