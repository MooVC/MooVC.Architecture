namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using System.Threading;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class AggregateAsyncEventArgs<TAggregate>
    : AsyncEventArgs,
      ISerializable
    where TAggregate : AggregateRoot
{
    protected AggregateAsyncEventArgs(TAggregate aggregate, CancellationToken? cancellationToken = default)
        : base(cancellationToken: cancellationToken)
    {
        Aggregate = ArgumentNotNull(aggregate, nameof(aggregate), AggregateEventArgsAggregateRequired);
    }

    protected AggregateAsyncEventArgs(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.GetValue<TAggregate>(nameof(Aggregate));
    }

    public TAggregate Aggregate { get; }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Aggregate), Aggregate);
    }
}