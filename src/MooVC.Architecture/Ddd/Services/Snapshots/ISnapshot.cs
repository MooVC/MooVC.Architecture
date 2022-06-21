namespace MooVC.Architecture.Ddd.Services.Snapshots;

using System.Collections.Generic;
using MooVC.Architecture.Ddd.Services.Reconciliation;

public interface ISnapshot
{
    IEventSequence Sequence { get; }

    IEnumerable<EventCentricAggregateRoot> Aggregates { get; }
}