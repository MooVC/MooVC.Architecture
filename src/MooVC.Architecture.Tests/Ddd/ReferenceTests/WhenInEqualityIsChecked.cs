namespace MooVC.Architecture.Ddd.ReferenceTests;

using System;
using Xunit;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.ReferenceTests.TestableAggregate;

public class WhenInEqualityIsChecked
{
    [Fact]
    public void GivenAVersionedReferencedAndANonVersionedReferenceThatHaveToTheSameIdAndTypeThenBothAreConsideredEqual()
    {
        var aggregate = new SerializableAggregateRoot();

        Reference first = Create<SerializableAggregateRoot>(aggregate.Id);
        Reference second = Create(aggregate);

        Assert.False(first != second);
        Assert.False(second != first);
    }

    [Fact]
    public void GivenTwoSeparateInstancesWithTheSameIdAndTypeThenBothAreConsideredEqual()
    {
        var aggregateId = Guid.NewGuid();

        Reference first = Create<SerializableAggregateRoot>(aggregateId);
        Reference second = Create<SerializableAggregateRoot>(aggregateId);

        Assert.False(first != second);
        Assert.False(second != first);
    }

    [Fact]
    public void GivenTwoSeparateInstancesWithTheDifferentIdButSameTypeThenBothAreNotConsideredEqual()
    {
        Reference first = Create<SerializableAggregateRoot>(Guid.NewGuid());
        Reference second = Create<SerializableAggregateRoot>(Guid.NewGuid());

        Assert.True(first != second);
        Assert.True(second != first);
    }

    [Fact]
    public void GivenTwoSeparateInstancesWithTheSameIdButDifferentTypeThenBothAreNotConsideredEqual()
    {
        var aggregateId = Guid.NewGuid();

        Reference first = Create<SerializableAggregateRoot>(aggregateId);
        Reference second = Create<SerializableEventCentricAggregateRoot>(aggregateId);

        Assert.True(first != second);
        Assert.True(second != first);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenTwoTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(first: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(first: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(first: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFirstReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new One();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(first: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenTwoTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two>(second: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(second: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(second: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheSecondReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Two();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(second: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenThreeTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three>(third: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(third: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheThirdReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Three();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(third: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFourTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four>(fourth: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFourthReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Four();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fourth: reference);

        Assert.False(multi != reference);
    }

    [Fact]
    public void GivenTheFifthReferenceWhenFiveTypesArePermittedThenTheInstancesAreEqual()
    {
        var aggregate = new Five();
        var reference = aggregate.ToReference();
        var multi = new TestableReference<One, Two, Three, Four, Five>(fifth: reference);

        Assert.False(multi != reference);
    }
}