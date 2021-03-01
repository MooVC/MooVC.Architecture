namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public class WhenGetAllAsyncIsCalled
        : UnversionedMemoryRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenAnEmptyRepositoryThenAnEmptyEnumerableIsReturnedAsync(bool useCloner)
        {
            IRepository<SerializableAggregateRoot> repository = Create<SerializableAggregateRoot>(useCloner);
            IEnumerable<SerializableAggregateRoot> results = await repository.GetAllAsync();

            Assert.Empty(results);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenAPopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturnedAsync(bool useCloner)
        {
            const int ExpectedTotal = 2;

            var first = new SerializableEventCentricAggregateRoot();
            var second = new SerializableEventCentricAggregateRoot();

            IRepository<SerializableEventCentricAggregateRoot> repository =
                Create<SerializableEventCentricAggregateRoot>(useCloner);

            await repository.SaveAsync(first);
            await repository.SaveAsync(second);

            var context = new SerializableMessage();

            second.Set(new SetRequest(context, Guid.NewGuid()));

            await repository.SaveAsync(second);

            IEnumerable<SerializableEventCentricAggregateRoot> results = await repository.GetAllAsync();

            Assert.Equal(ExpectedTotal, results.Count());
            Assert.Contains(results, result => result.Id == first.Id && result.Version == first.Version);
            Assert.Contains(results, result => result.Id == second.Id && result.Version == second.Version);
        }
    }
}