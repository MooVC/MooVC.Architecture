namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using MooVC.Architecture.Serialization;
using Xunit;
using static MooVC.Architecture.Ddd.MultiTypeReferenceTests.TestableAggregate;

public sealed class WhenMultiTypeReferenceIsSerialized
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(first: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(first: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(first: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(second: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(second: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(second: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(second: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(third: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(third: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(third: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(fourth: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fourth: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheInstanceIsCloned(bool unversioned)
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fifth: reference, unversioned: unversioned);

        TestableMultiTypeReference<One, Two, Three, Four, Five> clone = multi.Clone();

        Assert.Equal(multi, clone);
    }
}