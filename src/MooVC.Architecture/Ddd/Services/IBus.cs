namespace MooVC.Architecture.Ddd.Services
{
    public interface IBus
    {
        event DomainEventsPublishedEventHandler Published;

        event DomainEventsPublishingEventHandler Publishing;

        event DomainEventsUnhandledEventHandler Unhandled;

        void Publish(params DomainEvent[] events);
    }
}