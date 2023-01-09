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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateEventSequenceUnorderedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }

    private static string FormatMessage(Reference aggregate, IEnumerable<DomainEvent> events)
    {
        _ = IsNotNull(aggregate, message: AggregateEventSequenceUnorderedExceptionAggregateRequired);
        _ = IsNotNull(events, message: AggregateEventSequenceUnorderedExceptionEventsRequired);

        return Format(AggregateEventSequenceUnorderedExceptionMessage, aggregate.Id, aggregate.Version, aggregate.Type.Name);
    }
}