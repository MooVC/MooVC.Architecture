namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using MooVC.Linq;
using static System.String;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;

public sealed class AggregateEventSequenceUnorderedException
    : ArgumentException
{
    internal AggregateEventSequenceUnorderedException(AggregateRoot aggregate, IEnumerable<DomainEvent> events)
        : this(Create(aggregate), events)
    {
    }

    internal AggregateEventSequenceUnorderedException(Reference aggregate, IEnumerable<DomainEvent> events)
        : base(FormatMessage(aggregate, events))
    {
        Aggregate = aggregate;
        Events = events.ToArrayOrEmpty();
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    private static string FormatMessage(Reference aggregate, IEnumerable<DomainEvent> events)
    {
        _ = Guard.Against.Null(aggregate, message: AggregateEventSequenceUnorderedExceptionAggregateRequired);
        _ = Guard.Against.Null(events, message: AggregateEventSequenceUnorderedExceptionEventsRequired);

        return AggregateEventSequenceUnorderedExceptionMessage.Format(aggregate.Id, aggregate.Version, aggregate.Type.Name);
    }
}