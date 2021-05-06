namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public interface IBus
    {
        event DomainEventsPublishedEventHandler Published;

        event DomainEventsPublishingEventHandler Publishing;

        Task PublishAsync(params DomainEvent[] events);
    }
}