namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Diagnostics;
    using static MooVC.Architecture.Ddd.Services.Resources;

    public abstract class Bus
        : IBus,
          IEmitDiagnostics
    {
        public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

        public event DomainEventsPublishedAsyncEventHandler? Published;

        public event DomainEventsPublishingAsyncEventHandler? Publishing;

        public virtual async Task PublishAsync(params DomainEvent[] events)
        {
            if (events.Any())
            {
                await OnPublishingAsync(events)
                    .ConfigureAwait(false);

                await PerformPublishAsync(events)
                    .ConfigureAwait(false);

                await OnPublishedAsync(events)
                    .ConfigureAwait(false);
            }
        }

        protected abstract Task PerformPublishAsync(DomainEvent[] events);

        protected virtual Task OnDiagnosticsEmittedAsync(
            Level level,
            Exception? cause = default,
            string? message = default)
        {
            return DiagnosticsEmitted.PassiveInvokeAsync(
                this,
                new DiagnosticsEmittedEventArgs(
                    cause: cause,
                    level: level,
                    message: message));
        }

        protected virtual Task OnPublishingAsync(params DomainEvent[] @events)
        {
            return Publishing.InvokeAsync(this, new DomainEventsPublishingEventArgs(@events));
        }

        protected virtual Task OnPublishedAsync(params DomainEvent[] @events)
        {
            return Published.PassiveInvokeAsync(
                this,
                new DomainEventsPublishedEventArgs(@events),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: BusOnPublishedAsyncFailure));
        }
    }
}