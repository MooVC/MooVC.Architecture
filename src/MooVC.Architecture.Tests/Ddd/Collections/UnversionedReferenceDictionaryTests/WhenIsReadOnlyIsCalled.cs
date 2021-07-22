namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using Xunit;

    public sealed class WhenIsReadOnlyIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryThenANegativeResponseIsReturned()
        {
            Assert.False(Dictionary.IsReadOnly);
        }
    }
}