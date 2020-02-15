namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class DomainEventUnhandledEventArgs
        : EventArgs
    {
        public DomainEventUnhandledEventArgs(DomainEvent @event, Action handler)
        {
            ArgumentNotNull(@event, nameof(@event), DomainEventUnhandledEventArgsEventRequired);
            ArgumentNotNull(handler, nameof(handler), DomainEventUnhandledEventArgsHandlerRequired);

            Event = @event;
            Handler = handler;
        }

        public DomainEvent Event { get; }

        public Action Handler { get; }
    }
}