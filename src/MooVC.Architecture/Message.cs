namespace MooVC.Architecture
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    [Serializable]
    public abstract class Message
        : Entity<Guid>
    {
        protected Message(Message? context = default)
            : base(Guid.NewGuid())
        {
            if (context is { })
            {
                CausationId = context.Id;
                CorrelationId = context.CorrelationId;
            }
        }

        protected Message(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            CausationId = info.TryGetValue<Guid>(nameof(CausationId));
            CorrelationId = info.GetValue<Guid>(nameof(CorrelationId));
            TimeStamp = info.GetDateTime(nameof(TimeStamp));
        }

        public Guid CausationId { get; } = Guid.Empty;

        public Guid CorrelationId { get; } = Guid.NewGuid();

        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddValue(nameof(CausationId), CausationId);
            info.AddValue(nameof(CorrelationId), CorrelationId);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}