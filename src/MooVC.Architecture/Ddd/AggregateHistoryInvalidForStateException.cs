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

[Serializable]
public sealed class AggregateHistoryInvalidForStateException
    : ArgumentException
{
    internal AggregateHistoryInvalidForStateException(AggregateRoot aggregate, IEnumerable<DomainEvent> events, SignedVersion startingVersion)
        : this(Create(aggregate), events, startingVersion)
    {
    }

    internal AggregateHistoryInvalidForStateException(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        SignedVersion startingVersion)
        : base(Format(AggregateHistoryInvalidForStateExceptionMessage, aggregate.Id, aggregate.Version, aggregate.Type.Name, startingVersion))
    {
        Aggregate = aggregate;
        Events = events.Snapshot();
        StartingVersion = startingVersion;
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateHistoryInvalidForStateException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        StartingVersion = info.TryGetValue(nameof(StartingVersion), defaultValue: SignedVersion.Empty);
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public SignedVersion StartingVersion { get; }

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
        _ = info.TryAddValue(nameof(StartingVersion), StartingVersion, defaultValue: SignedVersion.Empty);
    }
}