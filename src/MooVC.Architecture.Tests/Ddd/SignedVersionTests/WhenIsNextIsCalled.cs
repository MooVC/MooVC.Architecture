namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenIsNextIsCalled
{
    private readonly SerializableAggregateRoot aggregate;

    public WhenIsNextIsCalled()
    {
        aggregate = new SerializableAggregateRoot();
    }

    [Fact]
    public void GivenADifferentVersionThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        var other = new SerializableAggregateRoot();

        Assert.False(other.Version.IsNext(version));
    }

    [Fact]
    public void GivenADifferentNextVersionThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        var other = new SerializableAggregateRoot();
        Sequence next = other.Version.Next();

        Assert.False(next.IsNext(version));
    }

    [Fact]
    public void GivenAnEmptyVersionThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;

        Assert.False(version.IsNext(Sequence.Empty));
    }

    [Fact]
    public void GivenTheNextNextVersionThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next().Next();

        Assert.False(next.IsNext(version));
    }

    [Fact]
    public void GivenTheNextVersionThenTheResponseIsPositive()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.True(next.IsNext(version));
    }

    [Fact]
    public void GivenThePreviousVersionThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.False(version.IsNext(next));
    }

    [Fact]
    public void GivenADifferentVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        var other = new SerializableAggregateRoot();

        Assert.False(other.Version.IsNext(version.Footer, version.Number));
    }

    [Fact]
    public void GivenADifferentNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        var other = new SerializableAggregateRoot();
        Sequence next = other.Version.Next();

        Assert.False(next.IsNext(version.Footer, version.Number));
    }

    [Fact]
    public void GivenAnEmptyVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;

        Assert.False(version.IsNext(Sequence.Empty.Footer, Sequence.Empty.Number));
    }

    [Fact]
    public void GivenTheNextNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next().Next();

        Assert.False(next.IsNext(version.Footer, version.Number));
    }

    [Fact]
    public void GivenTheNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsPositive()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.True(next.IsNext(version.Footer, version.Number));
    }

    [Fact]
    public void GivenThePreviousVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
    {
        Sequence version = aggregate.Version;
        Sequence next = version.Next();

        Assert.False(version.IsNext(next.Footer, next.Number));
    }
}