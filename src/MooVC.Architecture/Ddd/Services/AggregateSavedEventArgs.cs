namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class AggregateSavedEventArgs<TAggregate>
        : AggregateEventArgs<TAggregate>
        where TAggregate : AggregateRoot
    {
        public AggregateSavedEventArgs(TAggregate aggregate)
            : base(aggregate)
        {
        }

        private AggregateSavedEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}