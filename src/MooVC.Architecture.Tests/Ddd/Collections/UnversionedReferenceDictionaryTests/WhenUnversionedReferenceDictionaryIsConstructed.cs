namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Collections.Generic;
    using Xunit;

    public sealed class WhenUnversionedReferenceDictionaryIsConstructed
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenNoExistingDictionaryThenAnEmptyInstanceIsReturned()
        {
            var instance = new UnversionedReferenceDictionary<SerializableAggregateRoot, int>();

            Assert.Empty(instance);
        }

        [Fact]
        public void GivenAnExistingDictionaryThenAnEmptyInstanceIsReturned()
        {
            IDictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot> existing = Dictionary.Snapshot();
            var instance = new UnversionedReferenceDictionary<SerializableAggregateRoot, SerializableAggregateRoot>(existing: existing);

            Assert.Equal(existing, instance);
        }
    }
}