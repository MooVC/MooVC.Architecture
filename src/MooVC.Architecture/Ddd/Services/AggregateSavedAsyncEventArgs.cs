namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using System.Threading;

[Serializable]
public sealed class AggregateSavedAsyncEventArgs<TAggregate>
    : AggregateAsyncEventArgs<TAggregate>
    where TAggregate : AggregateRoot
{
    public AggregateSavedAsyncEventArgs(
        TAggregate aggregate,
        CancellationToken? cancellationToken = default)
        : base(aggregate, cancellationToken: cancellationToken)
    {
    }

    private AggregateSavedAsyncEventArgs(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}