namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class DomainEvent
        : Message
    {
        protected DomainEvent(Message context, VersionedReference aggregate)
            : base(context)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), DomainEventAggregateRequired);

            Aggregate = aggregate;
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.GetValue<VersionedReference>(nameof(Aggregate));
        }

        public VersionedReference Aggregate { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
        }
    }
}