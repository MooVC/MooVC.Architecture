namespace MooVC.Architecture.EntityTests;

using System;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenInEqualityIsChecked
{
    [Fact]
    public void GivenTwoEntitiesWithTheSameIdAndTypeThenANegativeResponseIsReturned()
    {
        var first = new SerializableEntity<int>(1);
        SerializableEntity<int> second = first.Clone();

        Assert.False(first != second);
        Assert.False(second != first);
    }

    [Fact]
    public void GivenTwoEntitiesWithDifferentIdsAndTheSameTypeThenAPositiveResponseIsReturned()
    {
        var first = new SerializableEntity<int>(1);
        var second = new SerializableEntity<int>(2);

        Assert.True(first != second);
        Assert.True(second != first);
    }

    [Fact]
    public void GivenTwoEntitiesWithTheSameIdsButDifferentTypesThenAPositiveResponseIsReturned()
    {
        var first = new SerializableMessage();
        var second = new SerializableEntity<Guid>(first.Id);

        Assert.True(first != second);
        Assert.True(second != first);
    }
}