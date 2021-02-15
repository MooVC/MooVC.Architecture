namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveAsyncIsCalled
        : MemoryRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncrementedAsync(bool useCloner)
        {
            var expected = new SerializableAggregateRoot();

            MemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            await repository.SaveAsync(expected);

            SerializableAggregateRoot? actual = await repository.GetAsync(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown(bool useCloner)
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            MemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            await repository.SaveAsync(saved);

            AggregateConflictDetectedException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                    () => repository.SaveAsync(pending));

            Assert.Equal(saved.Id, exception.Aggregate.Id);
            Assert.Equal(saved.Version, exception.Persisted);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrementAsync(bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            MemoryRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            void Aggregate_Saved(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavedEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaved += Aggregate_Saved;

            await expectedRepository.SaveAsync(expectedAggregate);

            Assert.True(wasInvoked);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrementAsync(bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            MemoryRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            void Aggregate_Saving(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavingEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaving += Aggregate_Saving;

            await expectedRepository.SaveAsync(expectedAggregate);

            Assert.True(wasInvoked);
        }
    }
}