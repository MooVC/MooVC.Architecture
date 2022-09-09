namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using MooVC.Architecture.Serialization;
using Xunit;
using static MooVC.Architecture.Ddd.MultiTypeReferenceTests.TestableAggregate;

public sealed class WhenMultiTypeReferenceIsSerialized
{
    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(first: reference);

        TestableMultiTypeReference<One, Two> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(first: reference);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(first: reference);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(second: reference);

        TestableMultiTypeReference<One, Two> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(second: reference);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(second: reference);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(second: reference);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(third: reference);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(third: reference);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(third: reference);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(fourth: reference);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fourth: reference);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned()
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fifth: reference);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }
}