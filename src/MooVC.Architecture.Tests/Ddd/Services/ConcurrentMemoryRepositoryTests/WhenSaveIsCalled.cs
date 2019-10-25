namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveIsCalled
    {
        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncremented()
        {
            var expected = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(expected);
            SerializableAggregateRoot actual = repository.Get(expected.Id);

            Assert.Equal(expected, actual);
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown()
        {
            const ulong ExpectedVersion = 2;

            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(saved);
            AggregateConflictDetectedException<SerializableAggregateRoot> exception = Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.AggregateId);
            Assert.Equal(ExpectedVersion, exception.ExpectedVersion);
            Assert.Equal(saved.Version, exception.PersistedVersion);
        }

        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrement()
        {
            const ulong ExpectedVersion = AggregateRoot.DefaultVersion;

            var expectedAggregate = new SerializableAggregateRoot();
            var expectedRepository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();
            bool wasInvoked = false;

            void Aggregate_Saved(IRepository<SerializableAggregateRoot> actualRepository, AggregateSavedEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.Equal(ExpectedVersion, e.Aggregate.Version);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaved += Aggregate_Saved;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrement()
        {
            const ulong ExpectedVersion = AggregateRoot.DefaultVersion;

            var expectedAggregate = new SerializableAggregateRoot();
            var expectedRepository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();
            bool wasInvoked = false;

            void Aggregate_Saving(IRepository<SerializableAggregateRoot> actualRepository, AggregateSavingEventArgs<SerializableAggregateRoot> e)
            {
                Assert.Equal(expectedRepository, actualRepository);
                Assert.Equal(expectedAggregate, e.Aggregate);
                Assert.Same(expectedAggregate, e.Aggregate);
                Assert.Equal(ExpectedVersion, e.Aggregate.Version);

                wasInvoked = true;
            }

            expectedRepository.AggregateSaving += Aggregate_Saving;
            expectedRepository.Save(expectedAggregate);

            Assert.True(wasInvoked);
        }
    }
}