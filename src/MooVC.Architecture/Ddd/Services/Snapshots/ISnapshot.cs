namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public interface ISnapshot
        : ISerializable
    {
        IEventSequence Sequence { get; }

        IEnumerable<EventCentricAggregateRoot> Aggregates { get; }
    }
}