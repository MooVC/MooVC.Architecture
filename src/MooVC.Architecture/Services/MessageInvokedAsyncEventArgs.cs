namespace MooVC.Architecture.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Threading;

    [Serializable]
    public sealed class MessageInvokedAsyncEventArgs
        : MessageAsyncEventArgs
    {
        public MessageInvokedAsyncEventArgs(
            Message message,
            CancellationToken? cancellationToken = default)
            : base(message, cancellationToken: cancellationToken)
        {
        }

        private MessageInvokedAsyncEventArgs(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}