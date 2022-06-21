namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;

[Serializable]
public class SerializableAggregateRoot
    : AggregateRoot
{
    public SerializableAggregateRoot()
        : this(Guid.NewGuid())
    {
    }

    public SerializableAggregateRoot(Guid id)
        : base(id)
    {
    }

    private SerializableAggregateRoot(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public void Set()
    {
        MarkChangesAsUncommitted();
    }
}