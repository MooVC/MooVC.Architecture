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
        private readonly SerializableMessage context;
        private readonly Mock<IRepository<SerializableAggregateRoot>> repository;

        public WhenGetIsCalled()
        {
            context = new SerializableMessage();
            repository = new Mock<IRepository<SerializableAggregateRoot>>();
        }

        [Fact]
        public void GivenAReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheLatestAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var firstVersion = new SerializableAggregateRoot(aggregateId);
            var secondVersion = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<SignedVersion>(v => v == default)))
               .Returns(secondVersion);

            SerializableAggregateRoot value = repository.Object.Get(context, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(secondVersion, value);
        }

        [Fact]
        public void GivenAVersionedReferenceAndARequestForTheLatestWhenMoreThanOneVersionExistsThenTheRequestedVersionIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            SignedVersion firstVersion = aggregate.Version;
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            aggregate.MarkChangesAsCommitted();

            var context = new SerializableMessage();

            aggregate.Set();

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
                .Returns(default(SerializableAggregateRoot));

            var aggregateId = Guid.NewGuid();

            AggregateNotFoundException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.Get(context, aggregateId));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregateId, exception.Aggregate.Id);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public void GivenAnIdAndVersionThatExistsThenTheAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregate.Id), It.Is<SignedVersion>(v => v == aggregate.Version)))
               .Returns(aggregate);

            AggregateRoot value = repository.Object.Get(context, aggregate.Id, version: aggregate.Version);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(SerializableAggregateRoot));

            var reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            AggregateNotFoundException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.Get(context, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(SerializableAggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            AggregateVersionNotFoundException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateVersionNotFoundException<SerializableAggregateRoot>>(
                    () => repository.Object.Get(context, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(SerializableAggregateRoot));

            var reference = new Reference<SerializableEventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => repository.Object.Get(context, reference));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<SignedVersion>(v => v == default)))
               .Returns(aggregate);

            AggregateRoot value = repository.Object.Get(context, reference);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }
    }
}