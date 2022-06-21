namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Moq;
using Xunit;

public sealed class WhenExecuteAsyncIsCalled
{
    [Fact]
    public async Task GivenACommandThenTheAggregateIsSavedToTheRepositoryAsync()
    {
        var repository = new Mock<IRepository<AggregateRoot>>();
        var handler = new TestableCoordinatedGenerateHandler<Message>(repository.Object);
        var command = new SerializableMessage();

        await handler.ExecuteAsync(command, CancellationToken.None);

        repository.Verify(
            repo => repo.SaveAsync(
                It.IsAny<AggregateRoot>(),
                It.IsAny<CancellationToken?>()),
            Times.Once);
    }
}