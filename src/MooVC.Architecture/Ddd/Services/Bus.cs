namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Linq;

    public abstract class Bus
        : IBus
    {
        public event DomainEventsPublishedEventHandler Published;

        public event DomainEventsPublishingEventHandler Publishing;

        public event DomainEventsUnhandledEventHandler Unhandled;

        public void Publish(params DomainEvent[] events)
        {
            if (events.SafeAny())
            {
                OnPublishing(events);

                PerformPublish(events);

                OnPublished(events);
            }
        }

        protected abstract void PerformPublish(DomainEvent[] events);

        protected virtual void OnPublishing(params DomainEvent[] @events)
        {
            Publishing?.Invoke(this, new DomainEventsPublishingEventArgs(@events));
        }

        protected virtual void OnPublished(params DomainEvent[] @events)
        {
            Published?.Invoke(this, new DomainEventsPublishedEventArgs(@events));
        }

        protected virtual void OnUnhandled(Func<Task> handler, params DomainEvent[] @events)
        {
            Unhandled?.Invoke(this, new DomainEventsUnhandledEventArgs(@events, handler));
        }
    }
}