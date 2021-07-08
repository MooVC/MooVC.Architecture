namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using Xunit;

    public sealed class WhenKeysIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryWhenVersionedReferencesThenAllKeysAreUnversioned()
        {
            Assert.Equal(ExpectedCount, Dictionary.Keys.Count);
            Assert.Contains(Dictionary.Keys, element => element == FirstReference);
            Assert.Contains(Dictionary.Keys, element => element == SecondReference);
            Assert.DoesNotContain(Dictionary.Keys, element => element.IsVersioned);
        }
    }
}