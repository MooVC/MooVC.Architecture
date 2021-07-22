namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public delegate Task DomainEventsPublishedAsyncEventHandler(IBus sender, DomainEventsPublishedAsyncEventArgs e);
}