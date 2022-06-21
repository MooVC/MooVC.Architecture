namespace MooVC.Architecture.Ddd.Collections.DictionaryExtensionsTests;

using System.Collections.Generic;
using MooVC.Architecture.Ddd.ProjectionTests;
using Xunit;

public sealed class WhenToUnversionedIsCalled
{
    [Fact]
    public void GivenAnAggregateDictionaryThenAnUnversionedIsReturned()
    {
        var first = new SerializableAggregateRoot();
        var second = new SerializableAggregateRoot();

        var versioned = new Dictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>
        {
            { first.ToReference(), first },
            { second.ToReference(), second },
        };

        IDictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot> unversioned
            = versioned.ToUnversioned();

        Assert.Equal(versioned.Count, unversioned.Count);
        Assert.Contains(versioned, entry => unversioned.Contains(entry));
        Assert.DoesNotContain(unversioned, element => element.Key.IsVersioned);
    }

    [Fact]
    public void GivenAProjectionDictionaryThenAnUnversionedIsReturned()
    {
        var first = new SerializableAggregateRoot();
        var second = new SerializableAggregateRoot();

        var versioned = new Dictionary<Reference<SerializableAggregateRoot>, SerializableProjection<SerializableAggregateRoot>>
        {
            { first.ToReference(), new SerializableProjection<SerializableAggregateRoot>(first) },
            { second.ToReference(), new SerializableProjection<SerializableAggregateRoot>(second) },
        };

        IDictionary<Reference<SerializableAggregateRoot>, SerializableProjection<SerializableAggregateRoot>> unversioned
            = versioned.ToUnversioned();

        Assert.Equal(versioned.Count, unversioned.Count);
        Assert.Contains(versioned, entry => unversioned.Contains(entry));
        Assert.DoesNotContain(unversioned, element => element.Key.IsVersioned);
    }
}