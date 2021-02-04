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
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnEmptyRepositoryThenAnEmptyEnumerableIsReturned(bool useCloner)
        {
            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            IEnumerable<SerializableAggregateRoot> results = repository.GetAll();

            Assert.Empty(results);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAPopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturned(bool useCloner)
        {
            const int ExpectedTotal = 2;

            var first = new SerializableEventCentricAggregateRoot();
            var second = new SerializableEventCentricAggregateRoot();

            ConcurrentMemoryRepository<SerializableEventCentricAggregateRoot> repository =
                Create<SerializableEventCentricAggregateRoot>(useCloner);

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