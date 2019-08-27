namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class DomainEvent
        : Message
    {
        protected DomainEvent(Message context, IReference aggregate, ulong version)
            : base(context)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), DomainEventAggregateReferenceRequired);

            Aggregate = aggregate;
            Version = version;
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = (IReference)info.GetValue(nameof(Aggregate), typeof(IReference));
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        public IReference Aggregate { get; }

        public ulong Version { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Version), Version);
        }
    }
}