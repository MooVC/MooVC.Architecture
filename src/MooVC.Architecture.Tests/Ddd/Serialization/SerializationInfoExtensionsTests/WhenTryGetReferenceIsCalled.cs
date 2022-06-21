namespace MooVC.Architecture.Ddd.Serialization.SerializationInfoExtensionsTests;

using System.Runtime.Serialization;
using Xunit;

public sealed class WhenTryGetReferenceIsCalled
{
    private readonly SerializationInfo info;

    public WhenTryGetReferenceIsCalled()
    {
        info = new SerializationInfo(
            typeof(WhenTryAddReferenceIsCalled),
            new FormatterConverter());
    }

    [Fact]
    public void GivenAnEmptyTypedReferenceThenAnEmptyReferenceIsReturned()
    {
        Reference<SerializableAggregateRoot> original = Reference<SerializableAggregateRoot>.Empty;
        _ = info.TryAddReference(nameof(original), original);
        Reference<SerializableAggregateRoot> retrieved = info.TryGetReference<SerializableAggregateRoot>(nameof(original));

        Assert.Equal(original, retrieved);
    }

    [Fact]
    public void GivenAnEmptyUnTypedReferenceWhenNoDefaultIsSpecifiedThenAnEmptyUnTypedReferenceOfTheSameTypeIsReturned()
    {
        Reference original = Reference<SerializableAggregateRoot>.Empty;
        _ = info.TryAddReference(nameof(original), original);
        Reference retrieved = info.TryGetReference(nameof(original));

        Assert.Equal(original, retrieved);
    }

    [Fact]
    public void GivenAnEmptyUnTypedReferenceWhenADefaultIsSpecifiedThenAnEmptyUnTypedReferenceOfTheSameTypeIsReturned()
    {
        Reference original = Reference<SerializableAggregateRoot>.Empty;
        _ = info.TryAddReference(nameof(original), original);
        Reference retrieved = info.TryGetReference(nameof(original), defaultValue: Reference<EventCentricAggregateRoot>.Empty);

        Assert.Equal(original, retrieved);
    }

    [Fact]
    public void GivenATypedReferenceThenTheReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        var original = aggregate.ToReference();
        _ = info.TryAddReference(nameof(original), original);
        Reference<SerializableAggregateRoot> retrieved = info.TryGetReference<SerializableAggregateRoot>(nameof(original));

        Assert.Equal(original, retrieved);
    }

    [Fact]
    public void GivenAUnTypedReferenceThenTheReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        Reference original = aggregate.ToReference();
        _ = info.TryAddReference(nameof(original), original);
        Reference retrieved = info.TryGetReference(nameof(original));

        Assert.Equal(original, retrieved);
    }
}