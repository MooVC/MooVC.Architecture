namespace MooVC.Architecture.Ddd;

using System;

public static partial class GuidExtensions
{
    public static Reference ToReference(this Guid id, Type type, Sequence? version = default)
    {
        return Reference.Create(id, type, version: version);
    }

    public static Reference<TAggregate> ToReference<TAggregate>(this Guid id, Sequence? version = default)
        where TAggregate : AggregateRoot
    {
        return Reference<TAggregate>.Create(id, version: version);
    }
}