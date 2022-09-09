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

    [Fact]
    public void GivenNoReferencesWhenTwoTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two>(first: default));
    }

    [Fact]
    public void GivenNoReferencesWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three>(first: default));
    }

    [Fact]
    public void GivenNoReferencesWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four>(first: default));
    }

    [Fact]
    public void GivenNoReferencesWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: default));
    }

    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(first: reference);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(first: reference);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(first: reference);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(first: reference);

        Assert.Equal(reference, multi);
        Assert.True(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two>(second: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(second: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(second: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(second: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.True(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three>(third: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(third: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
        Assert.False(multi.IsFourth);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(third: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.True(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four>(fourth: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.True(multi.IsFourth);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fourth: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.True(multi.IsFourth);
        Assert.False(multi.IsFifth);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenAnInstanceIsCreated()
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableMultiTypeReference<One, Two, Three, Four, Five>(fifth: reference);

        Assert.Equal(reference, multi);
        Assert.False(multi.IsFirst);
        Assert.False(multi.IsSecond);
        Assert.False(multi.IsThird);
        Assert.False(multi.IsFourth);
        Assert.True(multi.IsFifth);
    }

    [Fact]
    public void GivenReferenceOneAndTwoWhenTwoTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two>(first: reference1, second: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndTwoWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three>(first: reference1, second: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndTwoWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, second: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndTwoWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Two();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four, Five>(
            first: reference1,
            second: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndThreeWhenThreeTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three>(first: reference1, third: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndThreeWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, third: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndThreeWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Three();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four, Five>(
            first: reference1,
            third: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndFourWhenFourTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Four();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four>(first: reference1, fourth: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndFourWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Four();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four, Five>(
            first: reference1,
            fourth: reference2));
    }

    [Fact]
    public void GivenReferenceOneAndFiveWhenFiveTypesArePermittedThenAnArgumentExceptionIsThrown()
    {
        var aggregate1 = new One();
        var aggregate2 = new Five();
        var reference1 = aggregate1.ToReference();
        var reference2 = aggregate2.ToReference();

        _ = Assert.Throws<ArgumentException>(() => new TestableMultiTypeReference<One, Two, Three, Four, Five>(
            first: reference1,
            fifth: reference2));
    }
}