namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static MooVC.Architecture.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class MessageEventArgs
        : EventArgs,
          ISerializable
    {
        protected MessageEventArgs(Message message)
        {
            ArgumentNotNull(message, nameof(message), MessageEventArgsMessageRequired);

            Message = message;
        }

        protected MessageEventArgs(SerializationInfo info, StreamingContext context)
        {
            Message = info.GetValue<Message>(nameof(Message));
        }

        public Message Message { get; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Message), Message);
        }
    }
}