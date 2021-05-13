namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public delegate Task MessageInvokedAsyncEventHandler(IBus bus, MessageInvokedEventArgs e);
}