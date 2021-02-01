namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveIsCalled
        : MemoryRepositoryTests
    {
        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncremented()
        {
            var expected = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(Cloner);

            repository.Save(expected);

            SerializableAggregateRoot? actual = repository.Get(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown()
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            var repository = new MemoryRepository<SerializableAggregateRoot>(Cloner);

            repository.Save(saved);

            AggregateConflictDetectedException<SerializableAggregateRoot> exception = Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.Aggregate.Id);
            Assert.Equal(saved.Version, exception.PersistedVersion);
        }

        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrement()
        {
            var expectedAggregate = new SerializableAggregateRoot();
            var expectedRepository = new MemoryRepository<SerializableAggregateRoot>(Cloner);
            bool wasInvoked = false;

            void Aggregate_Saved(IRepository<SerializableAggregateRoot> actualRepository, AggregateSavedEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaved += Aggregate_Saved;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrement()
        {
            var expectedAggregate = new SerializableAggregateRoot();
            var expectedRepository = new MemoryRepository<SerializableAggregateRoot>(Cloner);
            bool wasInvoked = false;

            void Aggregate_Saving(IRepository<SerializableAggregateRoot> actualRepository, AggregateSavingEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.True(e.Aggregate.Version.IsNew);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaving += Aggregate_Saving;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }
    }
}