namespace MooVC.Architecture.Ddd.Services
{
    public interface IBus
    {
        event DomainEventsPublishedEventHandler Published;

        event DomainEventsPublishingEventHandler Publishing;

        void Publish(params DomainEvent[] events);
    }
}