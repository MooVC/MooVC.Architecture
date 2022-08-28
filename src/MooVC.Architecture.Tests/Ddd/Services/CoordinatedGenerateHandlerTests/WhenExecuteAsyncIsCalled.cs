namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Threading;
using MooVC.Architecture.MessageTests;
using MooVC.Threading;
using Moq;
using Xunit;

public sealed class WhenExecuteAsyncIsCalled
{
    [Fact]
    public async Task GivenACommandThenTheAggregateIsSavedToTheRepositoryAsync()
    {
        var coordinator = new AggregateCoordinator<AggregateRoot>(new Coordinator<Guid>());
        var repository = new Mock<IRepository<AggregateRoot>>();
        var handler = new TestableCoordinatedGenerateHandler<Message>(coordinator, repository.Object);
        var command = new SerializableMessage();

        await handler.ExecuteAsync(command, CancellationToken.None);

        repository.Verify(
            repository => repository.SaveAsync(
                It.IsAny<AggregateRoot>(),
                It.IsAny<CancellationToken?>()),
            Times.Once);
    }
}