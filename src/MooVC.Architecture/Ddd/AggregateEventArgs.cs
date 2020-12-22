namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class AggregateEventArgs<TAggregate>
        : EventArgs,
          ISerializable
        where TAggregate : AggregateRoot
    {
        protected AggregateEventArgs(TAggregate aggregate)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), GenericAggregateRequired);

            Aggregate = aggregate;
        }

        protected AggregateEventArgs(SerializationInfo info, StreamingContext context)
        {
            Aggregate = info.GetValue<TAggregate>(nameof(Aggregate));
        }

        public TAggregate Aggregate { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Aggregate), Aggregate);
        }
    }
}