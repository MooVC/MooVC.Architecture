namespace MooVC.Architecture.Ddd.Collections.EnumerableExtensionsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.ProjectionTests;
    using Xunit;

    public sealed class WhenToUnversionedDictionaryIsCalled
    {
        [Fact]
        public void GivenAListOfAggregatesThenADictionaryContainingTheAggregatesIsReturned()
        {
            SerializableAggregateRoot[] aggregates = new[]
            {
                new SerializableAggregateRoot(),
                new SerializableAggregateRoot(),
                new SerializableAggregateRoot(),
            };

            IDictionary<Reference<SerializableAggregateRoot>, SerializableAggregateRoot> dictionary
                = aggregates.ToUnversionedDictionary();

            Assert.Equal(aggregates.Length, dictionary.Count);
            Assert.Contains(dictionary, element => element.Key.Id == element.Value.Id);
            Assert.DoesNotContain(dictionary, element => element.Key.IsVersioned);
        }

        [Fact]
        public void GivenAListOfProjectionsThenADictionaryContainingTheAggregatesIsReturned()
        {
            SerializableProjection<SerializableAggregateRoot>[] projections = new[]
            {
                new SerializableProjection<SerializableAggregateRoot>(new SerializableAggregateRoot()),
                new SerializableProjection<SerializableAggregateRoot>(new SerializableAggregateRoot()),
                new SerializableProjection<SerializableAggregateRoot>(new SerializableAggregateRoot()),
            };

            IDictionary<Reference<SerializableAggregateRoot>, SerializableProjection<SerializableAggregateRoot>> dictionary
                = projections.ToUnversionedDictionary<SerializableAggregateRoot, SerializableProjection<SerializableAggregateRoot>>();

            Assert.Equal(projections.Length, dictionary.Count);
            Assert.Contains(dictionary, element => element.Key == element.Value.Aggregate);
            Assert.DoesNotContain(dictionary, element => element.Key.IsVersioned);
        }
    }
}