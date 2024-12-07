namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenNextIsCalled
{
    private readonly SerializableAggregateRoot aggregate;

    public WhenNextIsCalled()
    {
        aggregate = new SerializableAggregateRoot();
    }

    [Fact]
    public void GivenAVersionThenTheNextVersionIsReturned()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.True(next.IsNext(version));
    }

    [Fact]
    public void GivenAVersionThenTheHeaderOfTheNextVersionIsTheFooterOfThePreviousVersion()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.Equal(version.Footer, next.Header);
    }

    [Fact]
    public void GivenAVersionThenTheHeaderOfTheNextVersionNumberIsOneHigherThanThePreviousVersion()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.True(next.Number - version.Number == 1);
    }
}