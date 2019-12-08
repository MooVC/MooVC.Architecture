namespace MooVC.Architecture.Services
{
    public sealed class MessageInvokingEventArgs
        : MessageEventArgs
    {
        public MessageInvokingEventArgs(Message message)
            : base(message)
        {
        }
    }
}