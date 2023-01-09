namespace MooVC.Architecture.Ddd.Services.Snapshots;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Services.Reconciliation;
using MooVC.Collections.Generic;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Snapshots.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class Snapshot
    : ISnapshot,
      ISerializable
{
    public Snapshot(IEnumerable<EventCentricAggregateRoot> aggregates, IEventSequence sequence)
    {
        Aggregates = aggregates.Snapshot();
        Sequence = IsNotNull(sequence, message: SnapshotSequenceRequired);
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private Snapshot(SerializationInfo info, StreamingContext context)
    {
        Aggregates = info.TryGetEnumerable<EventCentricAggregateRoot>(nameof(Aggregates));
        Sequence = info.GetValue<IEventSequence>(nameof(Sequence));
    }

    public IEnumerable<EventCentricAggregateRoot> Aggregates { get; }

    public IEventSequence Sequence { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Sequence), Sequence);
        _ = info.TryAddEnumerable(nameof(Aggregates), Aggregates);
    }
}