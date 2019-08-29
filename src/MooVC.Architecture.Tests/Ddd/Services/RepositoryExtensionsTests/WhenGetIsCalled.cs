namespace MooVC.Architecture.Ddd.Services.RepositoryExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class WhenGetIsCalled
    {
        private readonly Mock<Message> context;
        private readonly Mock<IRepository<AggregateRoot>> repository;

        public WhenGetIsCalled()
        {
            context = new Mock<Message>();
            repository = new Mock<IRepository<AggregateRoot>>();
        }

        public static IEnumerable<object[]> VersionData => new[]
        {
            new object[] { null },
            new object[] { 7ul }
        };

        [Fact]
        public void GivenAReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheLatestAggregateIsReturned()
        {
            const ulong VersionOne = 1,
                VersionTwo = 2;

            var aggregateId = Guid.NewGuid();
            var firstVersion = new Mock<AggregateRoot>(aggregateId, VersionOne);
            var secondVersion = new Mock<AggregateRoot>(aggregateId, VersionTwo);
            var reference = new Reference<AggregateRoot>(aggregateId, VersionOne);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<ulong?>(v => v == default)))
               .Returns(secondVersion.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference, getLatest: true);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(secondVersion.Object, value);
        }

        [Fact]
        public void GivenAnIdThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, aggregateId));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(aggregateId, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Theory]
        [MemberData(nameof(VersionData))]
        public void GivenAnIdAndVersionThatExistsThenTheAggregateIsReturned(ulong? version)
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, version);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<ulong?>(v => v == version)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, aggregateId, version: version);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);

            AggregateVersionNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateVersionNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(reference.Id, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotExistsWhenGetLatestIsRequestedThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference, getLatest: true));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(reference.Id, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Never);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<ulong?>(v => v == AggregateRoot.DefaultVersion)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }
    }
}