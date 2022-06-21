namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests;

using Xunit;

public sealed class WhenTheReferenceIndexerIsCalled
    : UnversionedReferenceDictionaryTests
{
    [Fact]
    public void GivenAVersionedReferenceThatDoesExistThenTheEntryIsReturned()
    {
        SerializableAggregateRoot aggregate = Dictionary[FirstReference];

        Assert.Same(FirstAggregate, aggregate);
    }

    [Fact]
    public void GivenAVersionedReferenceThatDoesNotExistThenTheEntryIsAddedWithAnUnversionedReference()
    {
        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();

        Dictionary[reference] = aggregate;

        Assert.Contains(Dictionary.Values, element => element == aggregate);
        Assert.Contains(Dictionary.Keys, element => element == reference);
        Assert.DoesNotContain(Dictionary.Keys, element => element.IsVersioned);
    }
}