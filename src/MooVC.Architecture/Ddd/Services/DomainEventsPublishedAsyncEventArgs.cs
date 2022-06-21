namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class DomainEventsPublishedAsyncEventArgs
    : DomainEventsAsyncEventArgs
{
    public DomainEventsPublishedAsyncEventArgs(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
        : base(events, cancellationToken: cancellationToken)
    {
    }

    private DomainEventsPublishedAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}