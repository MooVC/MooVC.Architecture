namespace MooVC.Architecture.Ddd.Services.CoordinatedOperationHandlerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenExecuteIsCalled
    {
        [Fact]
        public void GivenACommandThenTheAggregateIsRetrievedTheSetOperationInvokedAndTheChangesSaved()
        {
            var identity = Guid.NewGuid();
            var repository = new Mock<IRepository<SerializableEventCentricAggregateRoot>>();
            var handler = new TestableCoordinatedOperationHandler<Message>(identity, repository.Object);
            var command = new SerializableMessage();
            var aggregate = new SerializableEventCentricAggregateRoot(identity);

            Assert.NotEqual(identity, aggregate.Value);

            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion?>()))
                .Returns(aggregate);

            handler.Execute(command);

            Assert.Equal(identity, aggregate.Value);

            repository.Verify(
                repo => repo.Save(It.IsAny<SerializableEventCentricAggregateRoot>()),
                Times.Once);

            repository.Verify(
                repo => repo.Save(It.Is<SerializableEventCentricAggregateRoot>(source => source == aggregate)),
                Times.Once);
        }
    }
}