namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using Moq;
    using Xunit;

    public sealed class WhenSaveIsCalled
    {
        [Fact]
        public void GivenAnAggregateWithChangesThenSaveIsCalled()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            var aggregate = new SerializableAggregateRoot();

            aggregate.Save(repository.Object);

            repository.Verify(repo => repo.Save(It.IsAny<SerializableAggregateRoot>()), Times.Once);
        }

        [Fact]
        public void GivenAnAggregateWithNoChangesThenSaveIsNotCalled()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            var aggregate = new SerializableAggregateRoot();

            aggregate.MarkChangesAsCommitted();
            aggregate.Save(repository.Object);

            repository.Verify(repo => repo.Save(It.IsAny<SerializableAggregateRoot>()), Times.Never);
        }

        [Fact]
        public void GivenANullAggregateThenSaveIsNotCalled()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            SerializableAggregateRoot aggregate = default;

            aggregate.Save(repository.Object);

            repository.Verify(repo => repo.Save(It.IsAny<SerializableAggregateRoot>()), Times.Never);
        }
    }
}