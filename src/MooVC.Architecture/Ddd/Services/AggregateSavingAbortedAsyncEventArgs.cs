namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public sealed class AggregateSavingAbortedAsyncEventArgs<TAggregate>
    : AggregateAsyncEventArgs<TAggregate>
    where TAggregate : AggregateRoot
{
    public AggregateSavingAbortedAsyncEventArgs(TAggregate aggregate, Exception reason, CancellationToken? cancellationToken = default)
        : base(aggregate, cancellationToken: cancellationToken)
    {
        Reason = IsNotNull(reason, message: AggregateSavingAbortedAsyncEventArgsReasonRequired);
    }

    private AggregateSavingAbortedAsyncEventArgs(SerializationInfo info, StreamingContext context)
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