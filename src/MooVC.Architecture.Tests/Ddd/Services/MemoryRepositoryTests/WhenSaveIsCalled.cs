namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenSaveIsCalled
    {
        [Fact]
        public void GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncremented()
        {
            var expected = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>();

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

            var repository = new MemoryRepository<SerializableAggregateRoot>();

            repository.Save(saved);
            AggregateConflictDetectedException<SerializableAggregateRoot> exception = Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.Save(pending));

            Assert.Equal(saved.Id, exception.AggregateId);
            Assert.Equal(ExpectedVersion, exception.ExpectedVersion);
            Assert.Equal(saved.Version, exception.PersistedVersion);
        }
    }
}