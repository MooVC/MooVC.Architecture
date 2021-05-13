namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public interface IBus
    {
        event MessageInvokedAsyncEventHandler Invoked;

        event MessageInvokingAsyncEventHandler Invoking;

        Task InvokeAsync(Message message);
    }
}