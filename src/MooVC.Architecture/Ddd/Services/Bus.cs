namespace MooVC.Architecture.Ddd.Services
{
    using System.Linq;

    public abstract class Bus
        : IBus
    {
        public event DomainEventsPublishedEventHandler Published;

        public event DomainEventsPublishingEventHandler Publishing;

        public void Publish(params DomainEvent[] events)
        {
            if (events.Any())
            {
                Publishing?.Invoke(this, new DomainEventsPublishingEventArgs(events));

                PerformPublish(events);

                Published?.Invoke(this, new DomainEventsPublishedEventArgs(events));
            }
        }

        protected abstract void PerformPublish(DomainEvent[] events);
    }
}