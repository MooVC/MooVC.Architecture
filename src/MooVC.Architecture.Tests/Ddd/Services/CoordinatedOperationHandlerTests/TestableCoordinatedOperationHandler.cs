namespace MooVC.Architecture.Ddd.Services.CoordinatedOperationHandlerTests;

using System;

internal sealed class TestableCoordinatedOperationHandler<TCommand>
    : CoordinatedOperationHandler<SerializableEventCentricAggregateRoot, TCommand>
    where TCommand : Message
{
    private readonly Guid identity;

    public TestableCoordinatedOperationHandler(
        Guid identity,
        IRepository<SerializableEventCentricAggregateRoot> repository,
        TimeSpan? timeout = default)
        : base(repository, timeout)
    {
        this.identity = identity;
    }

    protected override Reference<SerializableEventCentricAggregateRoot> IdentifyTarget(TCommand message)
    {
        return identity.ToReference<SerializableEventCentricAggregateRoot>();
    }

    protected override void PerformCoordinatedOperation(
        SerializableEventCentricAggregateRoot aggregate,
        TCommand message)
    {
        var request = new SetRequest(message, identity);

        aggregate.Set(request);
    }
}