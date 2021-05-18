namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading;

    [Serializable]
    public sealed class AggregateSavingAsyncEventArgs<TAggregate>
        : AggregateAsyncEventArgs<TAggregate>
        where TAggregate : AggregateRoot
    {
        public AggregateSavingAsyncEventArgs(TAggregate aggregate, CancellationToken? cancellationToken = default)
            : base(aggregate, cancellationToken: cancellationToken)
        {
        }

        private AggregateSavingAsyncEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}