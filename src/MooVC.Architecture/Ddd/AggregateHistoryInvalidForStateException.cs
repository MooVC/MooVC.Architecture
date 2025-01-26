namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using MooVC.Linq;
using static System.String;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;

public sealed class AggregateHistoryInvalidForStateException
    : ArgumentException
{
    internal AggregateHistoryInvalidForStateException(AggregateRoot aggregate, IEnumerable<DomainEvent> events, Sequence startingVersion)
        : this(Create(aggregate), events, startingVersion)
    {
    }

    internal AggregateHistoryInvalidForStateException(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        Sequence startingVersion)
        : base(AggregateHistoryInvalidForStateExceptionMessage.Format(aggregate.Id, aggregate.Version, aggregate.Type.Name, startingVersion))
    {
        Aggregate = aggregate;
        Events = events.ToArrayOrEmpty();
        StartingVersion = startingVersion;
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public Sequence StartingVersion { get; }
}