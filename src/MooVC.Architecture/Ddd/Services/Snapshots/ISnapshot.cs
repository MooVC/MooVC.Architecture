namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd.Services.Reconciliation;

    public interface ISnapshot
        : ISerializable
    {
        IEventSequence Sequence { get; }

        IEnumerable<EventCentricAggregateRoot> Aggregates { get; }
    }
}