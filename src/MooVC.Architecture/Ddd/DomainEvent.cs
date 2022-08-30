namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class DomainEvent
    : Message
{
    protected DomainEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.GetValue<Reference>(nameof(Aggregate));
    }

    private protected DomainEvent(Reference aggregate, Message context)
        : base(context: context)
    {
        Aggregate = ArgumentNotNull(aggregate, nameof(aggregate), DomainEventAggregateRequired);
        _ = ArgumentNotNull(context, nameof(context), DomainEventContextRequired);
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