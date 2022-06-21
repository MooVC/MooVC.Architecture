namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests;

using Xunit;

public sealed class WhenClearIsCalled
    : UnversionedReferenceDictionaryTests
{
    [Fact]
    public void GivenAPopulatedDictionaryThenTheContentsAreRemoved()
    {
        Dictionary.Clear();

        Assert.Empty(Dictionary);
    }
}