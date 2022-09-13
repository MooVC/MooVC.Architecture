namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests;

using Xunit;

public sealed class WhenToUnversionedIsCalled
{
    [Fact]
    public void GivenANullUntypedVersionedReferenceThenANullValueIsReturned()
    {
        Reference? reference = default;
        Reference? unversioned = reference.ToUnversioned();

        Assert.Null(unversioned);
    }

    [Fact]
    public void GivenAnUntypedVersionedReferenceThenAnUntypedUnversionedReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();

        var original = Reference.Create(
            aggregate.Id,
            aggregate.GetType(),
            version: aggregate.Version);

        Reference unversioned = original.ToUnversioned();

        Assert.Equal(original, unversioned);
        Assert.NotSame(original, unversioned);
        Assert.True(original.IsVersioned);
        Assert.False(unversioned.IsVersioned);
    }

    [Fact]
    public void GivenANullTypedVersionedReferenceThenANullValueIsReturned()
    {
        Reference<SerializableAggregateRoot>? reference = default;
        Reference<SerializableAggregateRoot>? unversioned = reference.ToUnversioned();

        Assert.Null(unversioned);
    }

    [Fact]
    public void GivenAnUntypedUnversionedReferenceThenTheSameInstanceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        var original = Reference.Create(aggregate.Id, aggregate.GetType());

        Reference unversioned = original.ToUnversioned();

        Assert.Equal(original, unversioned);
        Assert.Same(original, unversioned);
        Assert.False(original.IsVersioned);
        Assert.False(unversioned.IsVersioned);
    }

    [Fact]
    public void GivenATypedVersionedReferenceThenAnUntypedUnversionedReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();

        var original = aggregate.ToReference();
        Reference unversioned = original.ToUnversioned();

        Assert.Equal(original, unversioned);
        Assert.NotSame(original, unversioned);
        Assert.True(original.IsVersioned);
        Assert.False(unversioned.IsVersioned);
    }

    [Fact]
    public void GivenATypedUnversionedReferenceThenTheSameInstanceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();

        var original = aggregate.ToReference(unversioned: true);
        Reference unversioned = original.ToUnversioned();

        Assert.Equal(original, unversioned);
        Assert.Same(original, unversioned);
        Assert.False(original.IsVersioned);
        Assert.False(unversioned.IsVersioned);
    }
}