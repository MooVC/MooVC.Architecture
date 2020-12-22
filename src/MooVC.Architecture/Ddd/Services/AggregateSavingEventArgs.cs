namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class AggregateSavingEventArgs<TAggregate>
        : AggregateEventArgs<TAggregate>
        where TAggregate : AggregateRoot
    {
        public AggregateSavingEventArgs(TAggregate aggregate)
            : base(aggregate)
        {
        }

        private AggregateSavingEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}