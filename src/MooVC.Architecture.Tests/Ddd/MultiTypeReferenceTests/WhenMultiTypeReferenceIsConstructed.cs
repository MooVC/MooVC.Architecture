namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;
using Xunit;
using static MooVC.Architecture.Ddd.MultiTypeReferenceTests.TestableAggregate;

public sealed class WhenMultiTypeReferenceIsConstructed
{
    [Fact]
    public void GivenADefaultInvocationWhenTwoTypesArePermittedThenAnEmptyReferenceIsCreated()
    {
        var empty = new TestableMultiTypeReference<One, Two>();
        Reference reference = empty;

        Assert.True(reference.IsEmpty);
    }

    [Fact]
    public void GivenADefaultInvocationWhenThreeTypesArePermittedThenAnEmptyReferenceIsCreated()
    {
        var empty = new TestableMultiTypeReference<One, Two, Three>();
        Reference reference = empty;

        Assert.True(reference.IsEmpty);
    }

    [Fact]
    public void GivenADefaultInvocationWhenFourTypesArePermittedThenAnEmptyReferenceIsCreated()
    {
        var empty = new TestableMultiTypeReference<One, Two, Three, Four>();
        Reference reference = empty;

        Assert.True(reference.IsEmpty);
    }

    [Fact]
    public void GivenADefaultInvocationWhenFiveTypesArePermittedThenAnEmptyReferenceIsCreated()
    {
        var empty = new TestableMultiTypeReference<One, Two, Three, Four, Five>();
        Reference reference = empty;

        Assert.True(reference.IsEmpty);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenNoReferencesWhenTwoTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two>(first: default, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenNoReferencesWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three>(first: default, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenNoReferencesWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four>(first: default, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenNoReferencesWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: default, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(first: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(first: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(first: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(second: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(second: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(second: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(second: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(third: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(third: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(third: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(fourth: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.True(multi.IsFourth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fourth: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.True(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated(bool unversioned)
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fifth: reference, unversioned: unversioned);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.True(multi.IsFifth);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndTwoWhenTwoTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two>(first: reference1, second: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndTwoWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three>(first: reference1, second: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndTwoWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, second: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndTwoWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference1, second: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndThreeWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three>(first: reference1, third: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndThreeWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, third: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndThreeWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference1, third: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndFourWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Four();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, fourth: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndFourWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Four();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference1, fourth: reference2, unversioned: unversioned));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void GivenReferenceOneAndFiveWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown(bool unversioned)
    {
        var aggregate1 = new One();
        var aggregate2 = new Five();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() =>
            new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference1, fifth: reference2, unversioned: unversioned));
    }
}