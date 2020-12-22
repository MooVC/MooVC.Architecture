namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class MessageInvokingEventArgs
        : MessageEventArgs
    {
        public MessageInvokingEventArgs(Message message)
            : base(message)
        {
        }

        private MessageInvokingEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}