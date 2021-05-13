namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public interface IBus
    {
        event DomainEventsPublishedAsyncEventHandler Published;

        event DomainEventsPublishingAsyncEventHandler Publishing;

        Task PublishAsync(params DomainEvent[] events);
    }
}