namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using Moq;
    using Xunit;

    public sealed class WhenSaveAsyncIsCalled
    {
        [Fact]
        public async Task GivenAnAggregateWithChangesThenSaveIsCalledAsync()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            var aggregate = new SerializableAggregateRoot();

            await aggregate.SaveAsync(repository.Object);

            repository.Verify(repo => repo.SaveAsync(It.IsAny<SerializableAggregateRoot>()), Times.Once);
        }

        [Fact]
        public async Task GivenAnAggregateWithChangesAndANullDestinaionThenAnArgumentNullExceptionIsThrownAsync()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = await Assert.ThrowsAsync<ArgumentNullException>(
                () => aggregate.SaveAsync(default!));
        }

        [Fact]
        public async Task GivenAnAggregateWithNoChangesThenSaveIsNotCalledAsync()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            var aggregate = new SerializableAggregateRoot();

            aggregate.MarkChangesAsCommitted();

            await aggregate.SaveAsync(repository.Object);

            repository.Verify(repo => repo.SaveAsync(It.IsAny<SerializableAggregateRoot>()), Times.Never);
        }

        [Fact]
        public async Task GivenAnAggregateWithNoChangesAndANullDestinaionThenNoExceptionIsThrownAsync()
        {
            var aggregate = new SerializableAggregateRoot();

            aggregate.MarkChangesAsCommitted();

            await aggregate.SaveAsync(default!);
        }

        [Fact]
        public async Task GivenANullAggregateThenSaveIsNotCalledAsync()
        {
            var repository = new Mock<IRepository<SerializableAggregateRoot>>();
            SerializableAggregateRoot? aggregate = default;

            await aggregate!.SaveAsync(repository.Object);

            repository.Verify(repo => repo.SaveAsync(It.IsAny<SerializableAggregateRoot>()), Times.Never);
        }
    }
}