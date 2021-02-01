namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.GetValue<VersionedReference>(nameof(Aggregate));
            Context = info.GetValue<Message>(nameof(Context));
            TimeStamp = info.GetDateTime(nameof(TimeStamp));
        }

        private protected DomainException(Message context, AggregateRoot aggregate, string message)
            : this(context, new VersionedReference(aggregate), message)
        {
        }

        private protected DomainException(Message context, VersionedReference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public VersionedReference Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Context), Context);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}