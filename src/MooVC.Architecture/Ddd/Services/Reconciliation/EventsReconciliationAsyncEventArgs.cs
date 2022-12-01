namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class EventsReconciliationAsyncEventArgs
    : EventsAsyncEventArgs,
      ISerializable
{
    public EventsReconciliationAsyncEventArgs(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
        : base(events, cancellationToken: cancellationToken)
    {
    }

    private EventsReconciliationAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}