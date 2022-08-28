namespace MooVC.Architecture.Ddd.Services.CoordinatedOperationHandlerTests;

using System;
using MooVC.Architecture.Ddd.Threading;

internal sealed class TestableCoordinatedOperationHandler<TCommand>
    : CoordinatedOperationHandler<SerializableEventCentricAggregateRoot, TCommand>
    where TCommand : Message
{
    private readonly Guid identity;

    public TestableCoordinatedOperationHandler(
        IAggregateCoordinator<SerializableEventCentricAggregateRoot> coordinator,
        Guid identity,
        IRepository<SerializableEventCentricAggregateRoot> repository)
        : base(coordinator, repository)
    {
        this.identity = identity;
    }

    protected override Reference<SerializableEventCentricAggregateRoot> IdentifyCoordinationContext(TCommand message)
    {
        return identity.ToReference<SerializableEventCentricAggregateRoot>();
    }

    protected override void Apply(SerializableEventCentricAggregateRoot aggregate, TCommand message)
    {
        var request = new SetRequest(message, identity);

        aggregate.Set(request);
    }
}