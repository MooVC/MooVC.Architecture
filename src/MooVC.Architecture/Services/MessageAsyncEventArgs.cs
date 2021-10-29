namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading;
    using MooVC.Serialization;
    using static MooVC.Architecture.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class MessageAsyncEventArgs
        : AsyncEventArgs,
          ISerializable
    {
        protected MessageAsyncEventArgs(
            Message message,
            CancellationToken? cancellationToken = default)
            : base(cancellationToken: cancellationToken)
        {
            Message = ArgumentNotNull(
                message,
                nameof(message),
                MessageEventArgsMessageRequired);
        }

        protected MessageAsyncEventArgs(SerializationInfo info, StreamingContext context)
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