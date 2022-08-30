namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;

[Serializable]
public abstract class DomainEvent<TAggregate>
    : DomainEvent
    where TAggregate : AggregateRoot
{
    private readonly Lazy<Reference<TAggregate>> aggregate;

    protected DomainEvent(Message context, TAggregate aggregate)
        : base(aggregate.ToReference(), context)
    {
        this.aggregate = new(GetTypedReference);
    }

    protected DomainEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        aggregate = new(GetTypedReference);
    }

    public new Reference<TAggregate> Aggregate => aggregate.Value;

    public static implicit operator Reference(DomainEvent<TAggregate>? @event)
    {
        return @event?.Aggregate ?? Reference<TAggregate>.Empty;
    }

    public static implicit operator Reference<TAggregate>(DomainEvent<TAggregate>? @event)
    {
        return @event?.Aggregate ?? Reference<TAggregate>.Empty;
    }

    private Reference<TAggregate> GetTypedReference()
    {
        return base.Aggregate.ToTyped<TAggregate>();
    }
}