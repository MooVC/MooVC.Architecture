namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class AggregateEventArgs<TAggregate>
        : EventArgs,
          ISerializable
        where TAggregate : AggregateRoot
    {
        protected AggregateEventArgs(TAggregate aggregate)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateEventArgsAggregateRequired);

            Aggregate = aggregate;
        }

        protected AggregateEventArgs(SerializationInfo info, StreamingContext context)
        {
            Aggregate = info.GetValue<TAggregate>(nameof(Aggregate));
        }

        public TAggregate Aggregate { get; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Aggregate), Aggregate);
        }
    }
}