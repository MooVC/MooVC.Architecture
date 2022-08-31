namespace MooVC.Architecture.Ddd.Services.CoordinatedOperationHandlerTests;

using System;
using MooVC.Architecture.Ddd.Threading;

internal sealed class TestableCoordinatedOperationHandler<TMessage>
    : CoordinatedOperationHandler<SerializableEventCentricAggregateRoot, TMessage>
    where TMessage : Message
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

    protected override Reference<SerializableEventCentricAggregateRoot> IdentifyCoordinationContext(TMessage message)
    {
        return identity.ToReference<SerializableEventCentricAggregateRoot>();
    }

    protected override void Apply(SerializableEventCentricAggregateRoot aggregate, TMessage message)
    {
        var request = new SetRequest(message, identity);

        aggregate.Set(request);
    }
}