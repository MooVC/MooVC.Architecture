namespace MooVC.Architecture.Ddd.Services.Snapshots
{
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
            ArgumentNotNull(sequence, nameof(sequence), SnapshotSequenceRequired);

            Aggregates = aggregates.Snapshot();
            Sequence = sequence;
        }

        private Snapshot(SerializationInfo info, StreamingContext context)
        {
            Aggregates = info.TryGetEnumerable<EventCentricAggregateRoot>(nameof(Aggregates));
            Sequence = info.GetValue<IEventSequence>(nameof(Sequence));
        }

        public IEnumerable<EventCentricAggregateRoot> Aggregates { get; }

        public IEventSequence Sequence { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Sequence), Sequence);
            _ = info.TryAddEnumerable(nameof(Aggregates), Aggregates);
        }
    }
}