namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenSignedVersionIsConstructed
{
    private readonly SerializableAggregateRoot aggregate;

    public WhenSignedVersionIsConstructed()
    {
        aggregate = new SerializableAggregateRoot();
    }

    [Fact]
    public void GivenANewVersionThenTheVersionIsFlaggedAsNew()
    {
        Assert.True(aggregate.Version.IsNew);
    }

    [Fact]
    public void GivenANewVersionThenTheVersionNumberIsSetToOne()
    {
        const ulong ExpectedVersionNumber = 1;

        Assert.Equal(ExpectedVersionNumber, aggregate.Version.Number);
    }

    [Fact]
    public void GivenANewVersionThenTheHeaderIsSetToAllZeros()
    {
        byte[] expectedHeader = new byte[8];

        Assert.Equal(expectedHeader, aggregate.Version.Header);
    }
}