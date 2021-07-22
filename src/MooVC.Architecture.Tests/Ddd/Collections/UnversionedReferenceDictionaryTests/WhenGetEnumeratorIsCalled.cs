namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenGetEnumeratorIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAPopulatedDictionaryWhenVersionedReferencesThenTheEnumeratorContainsUnversionedElements()
        {
            IEnumerator<KeyValuePair<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>> enumerator = Dictionary.GetEnumerator();

            int count = 0;
            var aggregates = Dictionary.Values.ToList();
            var references = Dictionary.Keys.ToList();

            while (enumerator.MoveNext())
            {
                Assert.False(enumerator.Current.Key.IsVersioned);
                Assert.Contains(aggregates, element => element == enumerator.Current.Value);
                Assert.Contains(references, element => element == enumerator.Current.Key);

                _ = aggregates.Remove(enumerator.Current.Value);
                _ = references.Remove(enumerator.Current.Key);

                count++;
            }

            Assert.Equal(ExpectedCount, count);
            Assert.Empty(aggregates);
            Assert.Empty(references);
        }
    }
}