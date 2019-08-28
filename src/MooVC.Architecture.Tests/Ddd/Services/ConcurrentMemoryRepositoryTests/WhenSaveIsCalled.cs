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

            Assert.NotSame(expected, actual);
            Assert.True(expected.Version - actual.Version == 1);
            Assert.Equal(expected.Id, actual.Id);
        }

        [Fact]
        public void GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrown()
        {
            var saved = new SerializableAggregateRoot();
            var pending = new SerializableAggregateRoot(saved.Id);

            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(saved);
            AggregateConflictDetectedException<SerializableAggregateRoot> exception = Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.AggregateId);
            Assert.Equal(saved.Version, exception.ExpectedVersion);
            Assert.Equal(pending.Version, exception.PersistedVersion);
        }
    }
}