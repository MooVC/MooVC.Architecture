namespace MooVC.Architecture.Ddd.Services.RepositoryExtensionsTests
{
    using System;
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

        [Fact]
        public void GivenAnIdThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, aggregateId));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(aggregateId, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAnIdThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, aggregateId);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(reference.Id, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);
            var reference = new Reference<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }
    }
}