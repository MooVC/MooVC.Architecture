namespace MooVC.Architecture.EntityTests;

using System;
using System.Runtime.Serialization;

[Serializable]
public sealed class SerializableEntity<T>
    : Entity<T>
    where T : notnull
{
    public SerializableEntity(T id)
        : base(id)
    {
    }

    private SerializableEntity(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}