namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenCopyToIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryThenTheUnversionedContentsAreCopied()
        {
            var array = new KeyValuePair<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>[ExpectedCount];
            Dictionary.CopyTo(array, 0);

            Assert.Equal(ExpectedCount, array.Length);
            Assert.Equal(Dictionary, array);
            Assert.Contains(array, element => element.Key == FirstReference);
            Assert.Contains(array, element => element.Value == FirstAggregate);
            Assert.Contains(array, element => element.Key == SecondReference);
            Assert.Contains(array, element => element.Value == SecondAggregate);
            Assert.DoesNotContain(array, element => element.Key.IsVersioned);
        }
    }
}