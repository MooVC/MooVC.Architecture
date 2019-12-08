namespace MooVC.Architecture.Services
{
    public sealed class MessageInvokedEventArgs
        : MessageEventArgs
    {
        public MessageInvokedEventArgs(Message message)
            : base(message)
        {
        }
    }
}