namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenContainsIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAVersionedReferenceThatExistThenAPositiveResponseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = aggregate.ToReference();
            var item = new KeyValuePair<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>(reference, aggregate);

            Dictionary.Add(item);

            bool exists = Dictionary.Contains(item);

            Assert.True(exists);
        }
    }
}