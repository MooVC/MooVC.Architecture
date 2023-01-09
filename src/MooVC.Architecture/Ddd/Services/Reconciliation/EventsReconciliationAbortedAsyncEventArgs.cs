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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private EventsReconciliationAbortedAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Reason = info.GetValue<Exception>(nameof(Reason));
    }

    public Exception Reason { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue(nameof(Reason), Reason);
    }
}