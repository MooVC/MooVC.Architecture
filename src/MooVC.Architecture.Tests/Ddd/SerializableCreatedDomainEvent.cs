namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;

[Serializable]
public sealed class SerializableCreatedDomainEvent
    : DomainEvent<SerializableEventCentricAggregateRoot>
{
    public SerializableCreatedDomainEvent(SerializableEventCentricAggregateRoot aggregate, Message context)
        : base(aggregate, context)
    {
    }

    private SerializableCreatedDomainEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}