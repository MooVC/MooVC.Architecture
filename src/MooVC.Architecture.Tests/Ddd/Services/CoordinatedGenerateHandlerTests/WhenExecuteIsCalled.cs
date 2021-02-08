namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests
{
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenExecuteIsCalled
    {
        [Fact]
        public void GivenACommandThenTheAggregateIsSavedToTheRepository()
        {
            var repository = new Mock<IRepository<AggregateRoot>>();
            var handler = new TestableCoordinatedGenerateHandler<Message>(repository.Object);
            var command = new SerializableMessage();

            handler.Execute(command);

            repository.Verify(repo => repo.Save(It.IsAny<AggregateRoot>()), Times.Once);
        }
    }
}