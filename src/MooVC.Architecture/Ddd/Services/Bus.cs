namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class Bus
        : IBus
    {
        public event DomainEventsPublishedEventHandler? Published;

        public event DomainEventsPublishingEventHandler? Publishing;

        public event DomainEventsUnhandledEventHandler? Unhandled;

        public virtual async Task PublishAsync(params DomainEvent[] events)
        {
            if (events.Any())
            {
                OnPublishing(events);

                await PerformPublishAsync(events)
                    .ConfigureAwait(false);

                OnPublished(events);
            }
        }

        protected abstract Task PerformPublishAsync(DomainEvent[] events);

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