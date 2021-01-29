﻿namespace MooVC.Architecture
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class Message
        : ISerializable
    {
        protected Message()
        {
        }

        protected Message(Message context)
        {
            ArgumentNotNull(context, nameof(context), MessageContextRequired);

            CausationId = context.Id;
            CorrelationId = context.CorrelationId;
        }

        protected Message(SerializationInfo info, StreamingContext context)
        {
            CausationId = info.TryGetValue<Guid>(nameof(CausationId));
            CorrelationId = info.GetValue<Guid>(nameof(CorrelationId));
            Id = info.GetValue<Guid>(nameof(Id));
            TimeStamp = info.GetDateTime(nameof(TimeStamp));
        }

        public Guid CausationId { get; } = Guid.Empty;

        public Guid CorrelationId { get; } = Guid.NewGuid();

        public Guid Id { get; } = Guid.NewGuid();

        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        public override bool Equals(object other)
        {
            return other is Message message
                && Id == message.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(CausationId), CausationId);
            info.AddValue(nameof(CorrelationId), CorrelationId);
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}