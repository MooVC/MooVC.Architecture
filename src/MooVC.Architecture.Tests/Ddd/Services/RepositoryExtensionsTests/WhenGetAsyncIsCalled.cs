namespace MooVC.Architecture.Ddd.Services.RepositoryExtensionsTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenGetAsyncIsCalled
    {
        private readonly SerializableMessage context;
        private readonly Mock<IRepository<SerializableAggregateRoot>> repository;

        public WhenGetAsyncIsCalled()
        {
            context = new SerializableMessage();
            repository = new Mock<IRepository<SerializableAggregateRoot>>();
        }

        [Fact]
        public async Task GivenAReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheLatestAggregateIsReturnedAsync()
        {
            var aggregateId = Guid.NewGuid();
            var firstVersion = new SerializableAggregateRoot(aggregateId);
            var secondVersion = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.GetAsync(
                   It.Is<Guid>(id => id == aggregateId),
                   It.IsAny<CancellationToken?>(),
                   It.Is<SignedVersion>(v => v == default)))
               .ReturnsAsync(secondVersion);

            SerializableAggregateRoot value = await repository.Object.GetAsync(context, reference);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(secondVersion, value);
        }

        [Fact]
        public async Task GivenAVersionedReferenceAndARequestForASpecificVersionWhenMoreThanOneVersionExistsThenTheRequestedVersionIsReturnedAsync()
        {
            var aggregate = new SerializableAggregateRoot();
            SignedVersion firstVersion = aggregate.Version;
            var reference = new Reference<SerializableAggregateRoot>(aggregate);

            aggregate.MarkChangesAsCommitted();

            var context = new SerializableMessage();

            aggregate.Set();

            _ = repository
               .Setup(repo => repo.GetAsync(
                   It.Is<Guid>(id => id == aggregate.Id),
                   It.IsAny<CancellationToken?>(),
                   It.Is<SignedVersion>(v => v == firstVersion)))
               .ReturnsAsync(aggregate);

            AggregateRoot value = await repository.Object.GetAsync(context, reference, latest: false);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Fact]
        public async Task GivenAnIdThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrownAsync()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var aggregateId = Guid.NewGuid();

            AggregateVersionNotFoundException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateVersionNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.GetAsync(context, aggregateId));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(aggregateId, exception.Aggregate.Id);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public async Task GivenAnIdAndVersionThatExistsThenTheAggregateIsReturnedAsync()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = repository
               .Setup(repo => repo.GetAsync(
                   It.Is<Guid>(id => id == aggregate.Id),
                   It.IsAny<CancellationToken?>(),
                   It.Is<SignedVersion>(v => v == aggregate.Version)))
               .ReturnsAsync(aggregate);

            AggregateRoot value = await repository.Object.GetAsync(
                context,
                aggregate.Id,
                version: aggregate.Version);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Fact]
        public async Task GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrownAsync()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            AggregateVersionNotFoundException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateVersionNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.GetAsync(context, reference));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public async Task GivenAVersionedReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrownAsync()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new Reference<SerializableAggregateRoot>(aggregate);

            AggregateVersionNotFoundException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateVersionNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.GetAsync(context, reference, latest: false));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public async Task GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrownAsync()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var reference = new Reference<SerializableEventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
                () => repository.Object.GetAsync(context, reference));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Never);
        }

        [Fact]
        public async Task GivenAReferenceThatExistsThenTheAggregateIsReturnedAsync()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.GetAsync(
                   It.Is<Guid>(id => id == aggregateId),
                   It.IsAny<CancellationToken?>(),
                   It.Is<SignedVersion>(v => v == default)))
               .ReturnsAsync(aggregate);

            AggregateRoot value = await repository.Object.GetAsync(context, reference);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(aggregate, value);
        }
    }
}