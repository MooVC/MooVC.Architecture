namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.Ddd.Serialization;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateConflictDetectedAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    internal AggregateConflictDetectedAsyncEventArgs(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        Sequence next,
        Sequence previous,
        CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Aggregate = IsNotEmpty(aggregate, message: AggregateConflictDetectedEventArgsAggregateRequired);
        Events = IsNotEmpty(events, message: AggregateConflictDetectedEventArgsEventsRequired);
        Next = IsNotNull(next, message: AggregateConflictDetectedEventArgsNextRequired);
        Previous = IsNotNull(previous, message: AggregateConflictDetectedEventArgsPreviousRequired);
    }

    private AggregateConflictDetectedAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        Next = info.GetValue<Sequence>(nameof(Next));
        Previous = info.GetValue<Sequence>(nameof(Previous));
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public Sequence Next { get; }

    public Sequence Previous { get; }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(Aggregate), Aggregate);
        _ = info.TryAddEnumerable(nameof(Events), Events);
        info.AddValue(nameof(Next), Next);
        info.AddValue(nameof(Previous), Previous);
    }
}