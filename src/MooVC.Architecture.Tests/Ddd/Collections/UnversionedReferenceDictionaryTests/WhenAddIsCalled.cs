namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests;

using System.Collections.Generic;
using Xunit;

public sealed class WhenAddIsCalled
    : UnversionedReferenceDictionaryTests
{
    [Fact]
    public void GivenAVersionedReferenceThatDoesNotExistThenTheEntryIsAddedWithAnUnversionedReference()
    {
        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();

        Dictionary.Add(reference, aggregate);

        Assert.Contains(Dictionary.Values, element => element == aggregate);
        Assert.Contains(Dictionary.Keys, element => element == reference);
        Assert.DoesNotContain(Dictionary.Keys, element => element.IsVersioned);
    }

    [Fact]
    public void GivenAVersionedReferenceThatDoesNotExistAsAKeyValuePairThenTheEntryIsAddedWithAnUnversionedReference()
    {
        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();
        var item = new KeyValuePair<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>(reference, aggregate);

        Dictionary.Add(item);

        Assert.Contains(item, Dictionary);
        Assert.Contains(Dictionary.Values, element => element == aggregate);
        Assert.Contains(Dictionary.Keys, element => element == reference);
        Assert.DoesNotContain(Dictionary.Keys, element => element.IsVersioned);
    }
}