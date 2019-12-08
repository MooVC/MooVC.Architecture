namespace MooVC.Architecture.Services
{
    public interface IBus
    {
        event MessageInvokedEventHandler Invoked;

        event MessageInvokingEventHandler Invoking;

        void Invoke(Message message);
    }
}