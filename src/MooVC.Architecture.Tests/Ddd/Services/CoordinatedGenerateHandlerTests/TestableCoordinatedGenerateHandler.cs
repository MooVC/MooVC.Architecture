namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests;

using MooVC.Architecture.Ddd.Threading;

internal sealed class TestableCoordinatedGenerateHandler<TCommand>
    : CoordinatedGenerateHandler<AggregateRoot, TCommand>
    where TCommand : Message
{
    public TestableCoordinatedGenerateHandler(IAggregateCoordinator<AggregateRoot> coordinator, IRepository<AggregateRoot> repository)
        : base(coordinator, repository)
    {
    }

    protected override AggregateRoot Generate(TCommand command)
    {
        return new SerializableEventCentricAggregateRoot(command.Id);
    }
}