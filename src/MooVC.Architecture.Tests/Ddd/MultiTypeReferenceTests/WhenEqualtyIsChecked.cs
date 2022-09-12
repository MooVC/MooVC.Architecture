namespace MooVC.Architecture.Ddd.ReferenceTests;

using Xunit;
using static MooVC.Architecture.Ddd.ReferenceTests.TestableAggregate;

public sealed class WhenEqualtyIsChecked
{
    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(first: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(first: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(first: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(first: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(second: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(second: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(second: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(second: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(third: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(third: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(third: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(fourth: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fourth: reference);

        Assert.True(multi == reference);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fifth: reference);

        Assert.True(multi == reference);
    }
}