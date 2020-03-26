namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(Message context, AggregateRoot aggregate, string message)
            : this(context, new VersionedReference(aggregate), message)
        {
        }

        protected DomainException(Message context, VersionedReference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.GetValue<VersionedReference>(nameof(Aggregate));
            Context = info.GetValue<Message>(nameof(Context));
            TimeStamp = info.GetDateTime(nameof(TimeStamp));
        }

        public VersionedReference Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Context), Context);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}