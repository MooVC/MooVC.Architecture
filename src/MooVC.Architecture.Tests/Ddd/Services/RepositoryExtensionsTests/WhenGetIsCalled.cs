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
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(reference.Id, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Theory]
        [MemberData(nameof(VersionData))]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned(ulong? version)
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, version);
            var reference = new Reference<AggregateRoot>(aggregateId, version: version);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<ulong?>(v => v == version)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }
    }
}