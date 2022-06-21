namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using MooVC.Collections.Generic;
using MooVC.Serialization;
using static System.String;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;
using static MooVC.Ensure;

[Serializable]
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
        Events = events.Snapshot();
    }

    private AggregateEventSequenceUnorderedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }

    private static string FormatMessage(Reference aggregate, IEnumerable<DomainEvent> events)
    {
        _ = ArgumentNotNull(
            aggregate,
            nameof(aggregate),
            AggregateEventSequenceUnorderedExceptionAggregateRequired);

        _ = ArgumentNotNull(
            events,
            nameof(events),
            AggregateEventSequenceUnorderedExceptionEventsRequired);

        return Format(
            AggregateEventSequenceUnorderedExceptionMessage,
            aggregate.Id,
            aggregate.Version,
            aggregate.Type.Name);
    }
}