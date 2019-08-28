namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenGetAllIsCalled
    {
        [Fact]
        public void GivenAnEmptyRepositoryThenAnEmptyEnumerableIsReturned()
        {
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();
            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Empty(results);
        }

        [Fact]
        public void GivenApopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturned()
        {
            const int FirstVersion = 1, 
                SecondVersion = 2,
                ExpectedTotal = 2;

            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();

            var firstAggregateVersionOne = new SerializableAggregateRoot(firstId, version: FirstVersion);
            var secondAggregateVersionOne = new SerializableAggregateRoot(secondId, version: FirstVersion);
            var secondAggregateVersionTwo = new SerializableAggregateRoot(secondId, version: SecondVersion);
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(firstAggregateVersionOne);
            repository.Save(secondAggregateVersionOne);
            repository.Save(secondAggregateVersionTwo);

            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Equal(ExpectedTotal, results.Count());
            Assert.Contains(results, result => result == firstAggregateVersionOne);
            Assert.Contains(results, result => result == secondAggregateVersionTwo);
        }
    }
}