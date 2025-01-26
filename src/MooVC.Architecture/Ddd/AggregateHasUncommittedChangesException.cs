namespace MooVC.Architecture.Ddd;

using System;
using static System.String;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;

public sealed class AggregateHasUncommittedChangesException
    : ArgumentException
{
    internal AggregateHasUncommittedChangesException(AggregateRoot aggregate)
        : this(Create(aggregate))
    {
    }

    internal AggregateHasUncommittedChangesException(Reference aggregate)
        : base(AggregateHasUncommittedChangesExceptionMessage.Format(aggregate.Id, aggregate.Version, aggregate.Type.Name))
    {
        Aggregate = aggregate;
    }

    public Reference Aggregate { get; }
}