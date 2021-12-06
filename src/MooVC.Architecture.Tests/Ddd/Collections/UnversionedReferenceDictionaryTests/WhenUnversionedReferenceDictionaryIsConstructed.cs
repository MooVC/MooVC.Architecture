namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
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
            IDictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot> existing =
                Dictionary.Snapshot();

            IDictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot> instance =
                new UnversionedReferenceDictionary<SerializableAggregateRoot, SerializableAggregateRoot>(existing: existing);

            Assert.NotSame(existing, instance);
            Assert.Equal(existing.Count, instance.Count);
            Assert.Contains(existing, existing => instance.Contains(existing));
        }
    }
}