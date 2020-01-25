namespace MooVC.Architecture.Ddd.Services.RepositoryExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
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
        public void GivenAReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheLatestAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var firstVersion = new Mock<AggregateRoot>(aggregateId);
            var secondVersion = new Mock<AggregateRoot>(aggregateId);
            var reference = new Reference<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<SignedVersion>(v => v == default)))
               .Returns(secondVersion.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(secondVersion.Object, value);
        }

        [Fact]
        public void GivenAVersionedReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheRequestedVersionIsReturned()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            SignedVersion firstVersion = aggregate.Version;
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            aggregate.MarkChangesAsCommitted();

            var context = new SerializableMessage();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregate.Id), It.Is<SignedVersion>(v => v == firstVersion)))
               .Returns(aggregate);

            AggregateRoot value = repository.Object.Get(context, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Fact]
        public void GivenAnIdThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, aggregateId));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregateId, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAnIdAndVersionThatExistsThenTheAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregate.Id), It.Is<SignedVersion>(v => v == aggregate.Version)))
               .Returns(aggregate);

            AggregateRoot value = repository.Object.Get(context.Object, aggregate.Id, version: aggregate.Version);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference.Id, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            AggregateVersionNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateVersionNotFoundException<AggregateRoot>>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => repository.Object.Get(context.Object, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId);
            var reference = new Reference<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<SignedVersion>(v => v == default)))
               .Returns(aggregate.Object);

            AggregateRoot value = repository.Object.Get(context.Object, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }
    }
}