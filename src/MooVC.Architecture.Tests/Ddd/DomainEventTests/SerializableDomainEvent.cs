namespace MooVC.Architecture.Ddd.DomainEventTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class SerializableDomainEvent<TAggregateRoot>
    : DomainEvent<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    public SerializableDomainEvent(TAggregateRoot aggregate, Message context)
        : base(aggregate, context)
    {
    }

    private SerializableDomainEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}