namespace MooVC.Architecture
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class Message
        : ISerializable
    {
        protected Message()
        {
        }

        protected Message(Message context)
        {
            Ensure.ArgumentNotNull(context, nameof(context), Resources.GenericContextRequired);

            CorrelationId = context.CorrelationId;
            CausationId = context.Id;
        }

        protected Message(SerializationInfo info, StreamingContext context)
        {
            CorrelationId = (Guid)info.GetValue(nameof(CorrelationId), typeof(Guid));
            CausationId = (Guid)info.GetValue(nameof(CausationId), typeof(Guid));
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
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

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(CorrelationId), CorrelationId);
            info.AddValue(nameof(CausationId), CausationId);
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(TimeStamp), TimeStamp);
        }
    }
}