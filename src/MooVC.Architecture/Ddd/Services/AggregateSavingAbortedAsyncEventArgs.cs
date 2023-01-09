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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    private AggregateSavingAbortedAsyncEventArgs(SerializationInfo info, StreamingContext context)
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