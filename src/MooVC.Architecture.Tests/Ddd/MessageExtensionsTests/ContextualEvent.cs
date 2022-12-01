namespace MooVC.Architecture.Ddd.MessageExtensionsTests;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture;

[Serializable]
internal sealed class ContextualEvent
    : DomainEvent<SerializableAggregateRoot>
{
    public ContextualEvent(SerializableAggregateRoot aggregate, Message context)
        : base(aggregate, context)
    {
    }

    private ContextualEvent(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}