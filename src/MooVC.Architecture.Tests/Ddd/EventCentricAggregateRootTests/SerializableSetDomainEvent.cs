namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    [Serializable]
    internal sealed class SerializableSetDomainEvent
        : DomainEvent
    {
        public SerializableSetDomainEvent(Message context, SerializableEventCentricAggregateRoot aggregate, Guid value)
            : base(context, aggregate.ToVersionedReference())
        {
            Value = value;
        }

        public SerializableSetDomainEvent(Message context, VersionedReference aggregate, Guid value)
            : base(context, aggregate)
        {
            Value = value;
        }

        private SerializableSetDomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = info.TryGetValue<Guid>(nameof(Value));
        }

        public Guid Value { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddValue(nameof(Value), Value);
        }
    }
}