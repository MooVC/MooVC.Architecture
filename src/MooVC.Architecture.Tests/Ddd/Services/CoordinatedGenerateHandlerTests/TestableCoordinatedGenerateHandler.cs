namespace MooVC.Architecture.Ddd.Services.CoordinatedGenerateHandlerTests;

using MooVC.Architecture.Ddd.Threading;

internal sealed class TestableCoordinatedGenerateHandler<TMessage>
    : CoordinatedGenerateHandler<AggregateRoot, TMessage>
    where TMessage : Message
{
    public TestableCoordinatedGenerateHandler(IAggregateCoordinator<AggregateRoot> coordinator, IRepository<AggregateRoot> repository)
        : base(coordinator, repository)
    {
    }

    protected override AggregateRoot? Generate(TMessage message)
    {
        return new SerializableEventCentricAggregateRoot(message.Id);
    }
}