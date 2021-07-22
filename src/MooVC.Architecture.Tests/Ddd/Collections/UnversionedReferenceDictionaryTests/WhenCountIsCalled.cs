namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using Xunit;

    public sealed class WhenCountIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryThenTheExpectedCountIsReturned()
        {
            Assert.Equal(ExpectedCount, Dictionary.Count);
        }
    }
}