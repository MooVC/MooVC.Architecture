namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class EventsAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    protected EventsAsyncEventArgs(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Events = IsNotEmpty(events, message: EventsAsyncEventArgsEventsRequired);
    }

    protected EventsAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public IEnumerable<DomainEvent> Events { get; }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }
}