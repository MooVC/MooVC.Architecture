namespace MooVC.Architecture.Ddd.MessageExtensionsTests;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture;

[Serializable]
internal sealed class ContextualMessage
    : Message<SerializableAggregateRoot>
{
    public ContextualMessage(Reference<SerializableAggregateRoot> aggregate, Message? context = default)
        : base(aggregate, context)
    {
    }

    private ContextualMessage(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}