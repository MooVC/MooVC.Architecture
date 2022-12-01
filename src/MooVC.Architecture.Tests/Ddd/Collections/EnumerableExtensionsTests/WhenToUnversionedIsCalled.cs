namespace MooVC.Architecture.Ddd.Collections.EnumerableExtensionsTests;

using System.Collections.Generic;
using System.Linq;
using Xunit;
using static MooVC.Architecture.Ddd.Reference;

public sealed class WhenToUnversionedIsCalled
{
    [Fact]
    public void GivenANullCollectionOfUntypedReferencesThenAnEmptyCollectionIsReturned()
    {
        IEnumerable<Reference>? originals = default;

        IEnumerable<Reference> unversioned = originals.ToUnversioned();

        Assert.Empty(unversioned);
    }

    [Fact]
    public void GivenACollectionOfUntypedReferencesThenACollectionOfUntypedReferencesIsReturned()
    {
        var first = new SerializableAggregateRoot();
        var second = new SerializableAggregateRoot();

        IEnumerable<Reference> originals = new[]
        {
            Create(first),
            Create(second),
        };

        IEnumerable<Reference> unversioned = originals.ToUnversioned();

        Assert.Equal(originals.Count(), unversioned.Count());
        Assert.All(originals, reference => Assert.True(reference.IsVersioned));
        Assert.All(unversioned, reference => Assert.False(reference.IsVersioned));

        Assert.Equal(originals.ElementAt(0), unversioned.ElementAt(0));
        Assert.Equal(originals.ElementAt(1), unversioned.ElementAt(1));
    }

    [Fact]
    public void GivenANullCollectionOfTypedReferencesThenAnEmptyCollectionIsReturned()
    {
        IEnumerable<Reference<SerializableAggregateRoot>>? originals = default;

        IEnumerable<Reference<SerializableAggregateRoot>> unversioned = originals.ToUnversioned();

        Assert.Empty(unversioned);
    }

    [Fact]
    public void GivenACollectionOfTypedReferencesThenACollectionOfTypedReferencesIsReturned()
    {
        var first = new SerializableAggregateRoot();
        var second = new SerializableAggregateRoot();

        IEnumerable<Reference<SerializableAggregateRoot>> originals = new[]
        {
            first.ToReference(),
            second.ToReference(),
        };

        IEnumerable<Reference<SerializableAggregateRoot>> unversioned = originals.ToUnversioned();

        Assert.Equal(originals.Count(), unversioned.Count());
        Assert.All(originals, reference => Assert.True(reference.IsVersioned));
        Assert.All(unversioned, reference => Assert.False(reference.IsVersioned));

        Assert.Equal(originals.ElementAt(0), unversioned.ElementAt(0));
        Assert.Equal(originals.ElementAt(1), unversioned.ElementAt(1));
    }
}