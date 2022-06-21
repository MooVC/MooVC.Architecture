namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests;

using Xunit;

public sealed class WhenTryGetValueIsCalled
    : UnversionedReferenceDictionaryTests
{
    [Fact]
    public void GivenAVersionedReferenceThatExistsThenTheEntryIsReturned()
    {
        bool exists = Dictionary.TryGetValue(SecondReference, out SerializableAggregateRoot? actual);

        Assert.True(exists);
        Assert.Same(SecondAggregate, actual);
    }

    [Fact]
    public void GivenAVersionedReferenceThatDoesNotExistsThenNoEntryIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();

        bool exists = Dictionary.TryGetValue(reference, out _);

        Assert.False(exists);
    }
}