namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class EventsReconciliationAbortedAsyncEventArgs
    : EventsAsyncEventArgs
{
    public EventsReconciliationAbortedAsyncEventArgs(
        IEnumerable<DomainEvent> events,
        Exception reason,
        CancellationToken? cancellationToken = default)
        : base(events, cancellationToken: cancellationToken)
    {
        Reason = IsNotNull(reason, message: EventsReconciliationAbortedAsyncEventArgsReasonRequired);
    }

    private EventsReconciliationAbortedAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Reason = info.GetValue<Exception>(nameof(Reason));
    }

    public Exception Reason { get; }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Reason), Reason);
    }
}