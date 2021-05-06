namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public interface IBus
    {
        event MessageInvokedEventHandler Invoked;

        event MessageInvokingEventHandler Invoking;

        Task InvokeAsync(Message message);
    }
}