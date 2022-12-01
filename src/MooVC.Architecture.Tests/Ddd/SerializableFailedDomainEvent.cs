namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;

[Serializable]
public sealed class SerializableFailedDomainEvent
    : DomainEvent<SerializableEventCentricAggregateRoot>
{
    public SerializableFailedDomainEvent(SerializableEventCentricAggregateRoot aggregate, Message context)
        : base(aggregate, context)
    {
    }

    private SerializableFailedDomainEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}