namespace MooVC.Architecture.Services
{
    using System;

    public abstract class MessageEventArgs
        : EventArgs
    {
        protected MessageEventArgs(Message message)
        {
            Message = message;
        }

        public Message Message { get; }
    }
}