namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenImplicitlyCastToULong
{
    [Fact]
    public void GivenAnEmptyVersionThenTheMinumumNumberIsReturned()
    {
        SignedVersion version = SignedVersion.Empty;
        ulong number = version;

        Assert.Equal(version.Number, number);
        Assert.Equal(ulong.MinValue, number);
    }

    [Fact]
    public void GivenAnVersionThenTheVersionNumberIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        SignedVersion version = aggregate.Version;
        ulong number = version;

        Assert.Equal(version.Number, number);
    }

    [Fact]
    public void GivenANullVersionThenTheMinumumNumberIsReturned()
    {
        SignedVersion? version = default;
        ulong number = version;

        Assert.Equal(ulong.MinValue, number);
    }
}