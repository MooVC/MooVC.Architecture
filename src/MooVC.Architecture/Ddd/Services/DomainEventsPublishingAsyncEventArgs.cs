namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class DomainEventsPublishingAsyncEventArgs
    : DomainEventsAsyncEventArgs
{
    public DomainEventsPublishingAsyncEventArgs(
        IEnumerable<DomainEvent> events,
        CancellationToken? cancellationToken = default)
        : base(events, cancellationToken: cancellationToken)
    {
    }

    private DomainEventsPublishingAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}