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
public sealed class AggregateReconciledAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    internal AggregateReconciledAsyncEventArgs(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Aggregate = ReferenceIsNotEmpty(
            aggregate,
            nameof(aggregate),
            AggregateReconciledEventArgsAggregateRequired);

        Events = ArgumentNotEmpty(
            events,
            nameof(events),
            AggregateReconciledEventArgsEventsRequired);
    }

    private AggregateReconciledAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.TryGetReference(nameof(Aggregate));
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public Reference Aggregate { get; }

    public IEnumerable<DomainEvent> Events { get; }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(Aggregate), Aggregate);
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }
}