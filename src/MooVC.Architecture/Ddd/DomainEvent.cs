namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class DomainEvent
        : Message
    {
        public const int DefaultVersion = 1;
        
        protected DomainEvent(Message context, IReference aggregate, int version = DomainEvent.DefaultVersion)
            : base(context)
        {
            Aggregate = aggregate;
            Version = version;
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = (IReference)info.GetValue(nameof(Aggregate), typeof(IReference));
            Version = info.GetInt32(nameof(Version));
        }

        public IReference Aggregate { get; }

        public int Version { get; } = DomainEvent.DefaultVersion;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Version), Version);
        }
    }
}