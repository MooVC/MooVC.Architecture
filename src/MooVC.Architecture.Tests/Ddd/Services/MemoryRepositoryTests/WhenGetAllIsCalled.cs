namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
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
            var repository = new MemoryRepository<SerializableAggregateRoot>();
            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Empty(results);
        }

        [Fact]
        public void GivenApopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturned()
        {
            const int ExpectedFirstVersion = 1,
                ExpectedSecondVersion = 2,
                ExpectedTotal = 2;

            var firstId = Guid.NewGuid();
            var secondId = Guid.NewGuid();

            var firstVersionOne = new SerializableAggregateRoot(firstId, version: ExpectedFirstVersion);
            var secondVersionOne = new SerializableAggregateRoot(secondId, version: ExpectedFirstVersion);
            var secondVersionTwo = new SerializableAggregateRoot(secondId, version: ExpectedSecondVersion);
            var repository = new MemoryRepository<SerializableAggregateRoot>();

            repository.Save(firstVersionOne);
            repository.Save(secondVersionOne);
            repository.Save(secondVersionTwo);

            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Equal(ExpectedTotal, results.Count());
            Assert.Contains(results, result => result.Id == firstId && result.Version == ExpectedFirstVersion);
            Assert.Contains(results, result => result.Id == secondId && result.Version == ExpectedSecondVersion);
        }
    }
}