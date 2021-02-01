namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenGetAllIsCalled
        : ConcurrentMemoryRepositoryTests
    {
        [Fact]
        public void GivenAnEmptyRepositoryThenAnEmptyEnumerableIsReturned()
        {
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>(Cloner);
            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Empty(results);
        }

        [Fact]
        public void GivenAPopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturned()
        {
            const int ExpectedTotal = 2;

            var first = new SerializableEventCentricAggregateRoot();
            var second = new SerializableEventCentricAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableEventCentricAggregateRoot>(Cloner);

            repository.Save(first);
            repository.Save(second);

            var context = new SerializableMessage();

            second.Set(new SetRequest(context, Guid.NewGuid()));

            repository.Save(second);

            IEnumerable<SerializableEventCentricAggregateRoot> results = repository.GetAll();

            Assert.Equal(ExpectedTotal, results.Count());
            Assert.Contains(results, result => result.Id == first.Id && result.Version == first.Version);
            Assert.Contains(results, result => result.Id == second.Id && result.Version == second.Version);
        }
    }
}