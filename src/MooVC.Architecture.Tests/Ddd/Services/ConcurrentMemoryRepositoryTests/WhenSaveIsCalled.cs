namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveIsCalled
        : ConcurrentMemoryRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncremented(bool useCloner)
        {
            var expected = new SerializableAggregateRoot();

            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            repository.Save(expected);

            SerializableAggregateRoot? actual = repository.Get(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown(bool useCloner)
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            repository.Save(saved);

            AggregateConflictDetectedException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                    () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.Aggregate.Id);
            Assert.Equal(saved.Version, exception.PersistedVersion);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrement(bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            ConcurrentMemoryRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            void Aggregate_Saved(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavedEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaved += Aggregate_Saved;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrement(bool useCloner)
        {
            var expectedAggregate = new SerializableAggregateRoot();
            bool wasInvoked = false;

            ConcurrentMemoryRepository<SerializableAggregateRoot> expectedRepository =
                Create<SerializableAggregateRoot>(useCloner);

            void Aggregate_Saving(
                IRepository<SerializableAggregateRoot> actualRepository,
                AggregateSavingEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaving += Aggregate_Saving;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }
    }
}