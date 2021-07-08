namespace MooVC.Architecture.Ddd.Collections.UnversionedReferenceDictionaryTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenRemoveIsCalled
        : UnversionedReferenceDictionaryTests
    {
        [Fact]
        public void GivenAVersionedReferenceThatExistsThenTheEntryIsRemoved()
        {
            bool removed = Dictionary.Remove(SecondReference);

            Assert.True(removed);
        }

        [Fact]
        public void GivenAVersionedReferenceThatExistAsAKeyValuePairThenTheEntryIsRemoved()
        {
            var item = new KeyValuePair<Reference<SerializableAggregateRoot>, SerializableAggregateRoot>(SecondReference, SecondAggregate);

            bool removed = Dictionary.Remove(item);

            Assert.True(removed);
        }
    }
}