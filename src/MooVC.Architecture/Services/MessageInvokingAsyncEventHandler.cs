namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public delegate Task MessageInvokingAsyncEventHandler(IBus bus, MessageInvokingEventArgs e);
}