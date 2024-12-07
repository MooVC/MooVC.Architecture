namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenImplicitlyCastToULong
{
    [Fact]
    public void GivenAnEmptyVersionThenTheMinumumNumberIsReturned()
    {
        Sequence version = Sequence.Empty;
        ulong number = version;

        Assert.Equal(version.Number, number);
        Assert.Equal(ulong.MinValue, number);
    }

    [Fact]
    public void GivenAnVersionThenTheVersionNumberIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        Sequence version = aggregate.Version;
        ulong number = version;

        Assert.Equal(version.Number, number);
    }

    [Fact]
    public void GivenANullVersionThenTheMinumumNumberIsReturned()
    {
        Sequence? version = default;
        ulong number = version;

        Assert.Equal(ulong.MinValue, number);
    }
}