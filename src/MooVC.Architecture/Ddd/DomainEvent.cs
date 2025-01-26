namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using Ardalis.GuardClauses;
using MooVC.Architecture.Cqrs;
using static MooVC.Architecture.Ddd.Resources;

public abstract record DomainEvent
    : Message
{
    private protected DomainEvent(Reference aggregate, Message context)
        : base(context)
    {
        Aggregate = Guard.Against.Null(aggregate, message: DomainEventAggregateRequired);
        _ = Guard.Against.Null(context, message: DomainEventContextRequired);
    }

    public Reference Aggregate { get; }

    public static implicit operator Reference(DomainEvent? @event)
    {
        return @event?.Aggregate ?? Reference<AggregateRoot>.Empty;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Aggregate), Aggregate);
    }
}