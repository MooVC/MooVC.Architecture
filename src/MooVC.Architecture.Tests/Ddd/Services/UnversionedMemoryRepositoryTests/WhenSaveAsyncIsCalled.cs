namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests
{
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public class WhenSaveAsyncIsCalled
        : UnversionedMemoryRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncrementedAsync(bool useCloner)
        {
            var expected = new SerializableAggregateRoot();

            IRepository<SerializableAggregateRoot> repository =
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
        public async Task GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrownAsync(bool useCloner)
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            IRepository<SerializableAggregateRoot> repository =
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
        public async Task GivenANonNewAnAggregateWhenNoExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrownAsync(bool useCloner)
        {
            var aggregate = new SerializableAggregateRoot();

            aggregate.MarkChangesAsCommitted();
            aggregate.Set();

            IRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            AggregateConflictDetectedException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                    () => repository.SaveAsync(aggregate));

            Assert.Equal(aggregate.Id, exception.Aggregate.Id);
            Assert.Equal(SignedVersion.Empty, exception.Persisted);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenAnAggregateWhenAnExistingMemberHasAHigherVersionThenAnAggregateConflictDetectedExceptionIsThrownAsync(bool useCloner)
        {
            var saved = new SerializableAggregateRoot();

            IRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            await repository.SaveAsync(saved);

            SerializableAggregateRoot pending = saved.Clone();

            saved.Set();

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
        public async Task GivenAnAggregateWhenAnExistingMemberHasALowerVersionThenAnAggregateConflictDetectedExceptionIsThrownAsync(bool useCloner)
        {
            var saved = new SerializableAggregateRoot();

            IRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            await repository.SaveAsync(saved);

            SerializableAggregateRoot pending = saved.Clone();

            pending.Set();
            pending.MarkChangesAsCommitted();
            pending.Set();
            pending.MarkChangesAsCommitted();

            AggregateConflictDetectedException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                    () => repository.SaveAsync(pending));

            Assert.Equal(saved.Id, exception.Aggregate.Id);
            Assert.Equal(saved.Version, exception.Persisted);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrementAsync(
            bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            IRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            Task Aggregate_Saved(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavedEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                return Task.FromResult(wasInvoked = true);
            }

            expectedRepository.AggregateSaved += Aggregate_Saved;

            await expectedRepository.SaveAsync(expectedAggregate);

            Assert.True(wasInvoked);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrementAsync(
            bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            IRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            Task Aggregate_Saving(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavingEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                return Task.FromResult(wasInvoked = true);
            }

            expectedRepository.AggregateSaving += Aggregate_Saving;

            await expectedRepository.SaveAsync(expectedAggregate);

            Assert.True(wasInvoked);
        }
    }
}