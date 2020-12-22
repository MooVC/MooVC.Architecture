namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class MessageInvokedEventArgs
        : MessageEventArgs
    {
        public MessageInvokedEventArgs(Message message)
            : base(message)
        {
        }

        private MessageInvokedEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}