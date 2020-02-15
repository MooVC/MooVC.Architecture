namespace MooVC.Architecture.Ddd.Services
{
    using MooVC.Logging;

    public interface IBus
    {
        event DomainEventsPublishedEventHandler Published;

        event DomainEventsPublishingEventHandler Publishing;

        event DomainEventUnhandledEventHandler Unhandled;

        void Publish(params DomainEvent[] events);
    }
}