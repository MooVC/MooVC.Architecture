namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using Xunit;

    public sealed class WhenValuesIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryThenAllValuesAreReturn()
        {
            Assert.Equal(ExpectedCount, Dictionary.Values.Count);
            Assert.Contains(Dictionary.Values, element => element == FirstAggregate);
            Assert.Contains(Dictionary.Values, element => element == SecondAggregate);
        }
    }
}