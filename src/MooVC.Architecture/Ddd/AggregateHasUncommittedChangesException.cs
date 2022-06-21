namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;

[Serializable]
public sealed class AggregateHasUncommittedChangesException
    : ArgumentException
{
    internal AggregateHasUncommittedChangesException(AggregateRoot aggregate)
        : this(Create(aggregate))
    {
    }

    internal AggregateHasUncommittedChangesException(Reference aggregate)
        : base(Format(
            AggregateHasUncommittedChangesExceptionMessage,
            aggregate.Id,
            aggregate.Version,
            aggregate.Type.Name))
    {
        Aggregate = aggregate;
    }

    private AggregateHasUncommittedChangesException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
    }

    public Reference Aggregate { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
    }
}