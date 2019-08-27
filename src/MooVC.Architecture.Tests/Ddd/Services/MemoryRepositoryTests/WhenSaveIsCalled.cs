namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveIsCalled
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncremented(bool isThreadSafe)
        {
            var expected = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(expected);
            SerializableAggregateRoot actual = repository.Get(expected.Id);

            Assert.NotSame(expected, actual);
            Assert.True(expected.Version - actual.Version == 1);
            Assert.Equal(expected.Id, actual.Id);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown(bool isThreadSafe)
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(saved);
            AggregateConflictDetectedException<SerializableAggregateRoot> exception = Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.AggregateId);
            Assert.Equal(saved.Version, exception.ExpectedVersion);
            Assert.Equal(pending.Version, exception.PersistedVersion);
        }
    }
}