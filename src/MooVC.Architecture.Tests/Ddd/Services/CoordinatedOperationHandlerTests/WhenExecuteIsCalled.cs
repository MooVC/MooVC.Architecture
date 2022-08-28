namespace MooVC.Architecture.Ddd.Services.CoordinatedOperationHandlerTests;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Threading;
using MooVC.Architecture.MessageTests;
using MooVC.Threading;
using Moq;
using Xunit;

public sealed class WhenExecuteIsCalled
{
    [Fact]
    public async Task GivenACommandThenTheAggregateIsRetrievedTheSetOperationInvokedAndTheChangesSavedAsync()
    {
        var identity = Guid.NewGuid();
        var coordinator = new AggregateCoordinator<SerializableEventCentricAggregateRoot>(new Coordinator<Guid>());
        var repository = new Mock<IRepository<SerializableEventCentricAggregateRoot>>();
        var handler = new TestableCoordinatedOperationHandler<Message>(coordinator, identity, repository.Object);
        var command = new SerializableMessage();
        var aggregate = new SerializableEventCentricAggregateRoot(identity);

        Assert.NotEqual(identity, aggregate.Value);

        _ = repository
            .Setup(repository => repository.GetAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken?>(),
                It.IsAny<SignedVersion?>()))
            .ReturnsAsync(aggregate);

        await handler.ExecuteAsync(command, CancellationToken.None);

        Assert.Equal(identity, aggregate.Value);

        repository.Verify(
            repository => repository.SaveAsync(
                It.IsAny<SerializableEventCentricAggregateRoot>(),
                It.IsAny<CancellationToken?>()),
            Times.Once);

        repository.Verify(
            repository => repository.SaveAsync(
                It.Is<SerializableEventCentricAggregateRoot>(source => source == aggregate),
                It.IsAny<CancellationToken?>()),
            Times.Once);
    }
}