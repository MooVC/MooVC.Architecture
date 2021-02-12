namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests
{
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

            await handler.ExecuteAsync(command);

            repository.Verify(repo => repo.Save(It.IsAny<AggregateRoot>()), Times.Once);
        }
    }
}