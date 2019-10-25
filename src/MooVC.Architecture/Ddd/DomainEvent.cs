namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class DomainEvent
        : Message
    {
        protected DomainEvent(Message context, AggregateRoot aggregate)
            : base(context)
        {
            Aggregate = aggregate.ToVersionedReference();
        }

        protected DomainEvent(Message context, VersionedReference aggregate)
            : base(context)
        {
            Aggregate = aggregate;
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = (VersionedReference)info.GetValue(nameof(Aggregate), typeof(VersionedReference));
        }

        public VersionedReference Aggregate { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
        }
    }
}