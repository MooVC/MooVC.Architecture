namespace MooVC.Architecture.Ddd.SignedVersionTests;

using Xunit;

public sealed class WhenCompareToIsCalled
{
    private readonly SerializableAggregateRoot aggregate;

    public WhenCompareToIsCalled()
    {
        aggregate = new SerializableAggregateRoot();
    }

    [Fact]
    public void GivenAnEarlierVersionThenPositiveOneIsReturned()
    {
        const int ExpectedValue = 1;

        Sequence version = aggregate.Version;
        int actualValue = version.CompareTo(Sequence.Empty);

        Assert.Equal(ExpectedValue, actualValue);
    }

    [Fact]
    public void GivenALaterVersionThenNegativeOneIsReturned()
    {
        const int ExpectedValue = -1;

        Sequence version = aggregate.Version;
        int actualValue = Sequence.Empty.CompareTo(version);

        Assert.Equal(ExpectedValue, actualValue);
    }

    [Fact]
    public void GivenANullVersionThenPositiveOneIsReturned()
    {
        const int ExpectedValue = 1;

        Sequence version = aggregate.Version;
        int actualValue = version.CompareTo(default);

        Assert.Equal(ExpectedValue, actualValue);
    }

    [Fact]
    public void GivenAFutureVersionThenNegativeOneIsReturned()
    {
        const int ExpectedValue = -1;

        Sequence version = aggregate.Version;
        Sequence future = version.Next().Next();
        int actualValue = version.CompareTo(future);

        Assert.Equal(ExpectedValue, actualValue);
    }

    [Fact]
    public void GivenTheNextVersionThenNegativeOneIsReturned()
    {
        const int ExpectedValue = -1;

        Sequence version = aggregate.Version;
        Sequence next = version.Next();
        int actualValue = version.CompareTo(next);

        Assert.Equal(ExpectedValue, actualValue);
    }

    [Fact]
    public void GivenThePreviousVersionThenPositiveOneIsReturned()
    {
        const int ExpectedValue = 1;

        Sequence version = aggregate.Version;
        Sequence next = version.Next();
        int actualValue = next.CompareTo(version);

        Assert.Equal(ExpectedValue, actualValue);
    }
}