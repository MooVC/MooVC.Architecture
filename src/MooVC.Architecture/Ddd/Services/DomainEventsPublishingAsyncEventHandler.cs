namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public delegate Task DomainEventsPublishingAsyncEventHandler(IBus sender, DomainEventsPublishingEventArgs e);
}