namespace MooVC.Architecture.Ddd.ReferenceTests;

using Xunit;
using static MooVC.Architecture.Ddd.ReferenceTests.TestableAggregate;

public sealed class WhenImplicitlyCastToReference
{
    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheReferenceIsCast()
    {
        var aggregate = new One();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(first: expected);

        Reference actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheReferenceIsCast()
    {
        var aggregate = new Two();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(second: expected);

        Reference actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheReferenceIsCast()
    {
        var aggregate = new Three();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(third: expected);

        Reference actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheReferenceIsCast()
    {
        var aggregate = new Four();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(fourth: expected);

        Reference actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheReferenceIsCast()
    {
        var aggregate = new Five();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fifth: expected);

        Reference actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheOneReferenceIsCast()
    {
        var aggregate = new One();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(first: expected);

        Reference<One> actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheTwoReferenceIsCast()
    {
        var aggregate = new Two();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(second: expected);

        Reference<Two> actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheThreeReferenceIsCast()
    {
        var aggregate = new Three();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(third: expected);

        Reference<Three> actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheFourReferenceIsCast()
    {
        var aggregate = new Four();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(fourth: expected);

        Reference<Four> actual = multi;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheFiveReferenceIsCast()
    {
        var aggregate = new Five();
        var expected = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fifth: expected);

        Reference<Five> actual = multi;

        Assert.Equal(expected, actual);
    }
}