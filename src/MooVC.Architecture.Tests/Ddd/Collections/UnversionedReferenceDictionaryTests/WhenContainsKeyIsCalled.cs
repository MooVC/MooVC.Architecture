namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using Xunit;

    public sealed class WhenContainsKeyIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAVersionedReferenceThatExistThenAPositiveResponseIsReturned()
        {
            bool exists = Dictionary.ContainsKey(FirstReference);

            Assert.True(exists);
        }
    }
}