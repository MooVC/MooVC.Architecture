namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Collections.Generic;
using MooVC.Serialization;

[Serializable]
public abstract class DomainEventsAsyncEventArgs
    : AsyncEventArgs,
      ISerializable
{
    protected DomainEventsAsyncEventArgs(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Events = events.Snapshot();
    }

    protected DomainEventsAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
    }

    public IEnumerable<DomainEvent> Events { get; }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddEnumerable(nameof(Events), Events);
    }
}