namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading;

    [Serializable]
    public sealed class MessageInvokingAsyncEventArgs
        : MessageAsyncEventArgs
    {
        public MessageInvokingAsyncEventArgs(Message message, CancellationToken? cancellationToken = default)
            : base(message, cancellationToken: cancellationToken)
        {
        }

        private MessageInvokingAsyncEventArgs(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}