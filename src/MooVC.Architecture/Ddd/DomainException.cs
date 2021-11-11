namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetReference(nameof(Aggregate));
            Context = info.GetValue<Message>(nameof(Context));
            TimeStamp = info.GetValue<DateTimeOffset>(nameof(TimeStamp));
        }

        private protected DomainException(Message context, Reference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public Reference Aggregate { get; }

        public Message Context { get; }

        public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddReference(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Context), Context);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}