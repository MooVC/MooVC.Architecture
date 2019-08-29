namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    internal sealed class SerializableSetDomainEvent
        : DomainEvent
    {
        public SerializableSetDomainEvent(Message context, SerializableEventCentricAggregateRoot aggregate, Guid value)
            : base(context, aggregate)
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
            Value = (Guid)info.GetValue(nameof(Value), typeof(Guid));
        }

        public Guid Value { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Value), Value);
        }
    }
}