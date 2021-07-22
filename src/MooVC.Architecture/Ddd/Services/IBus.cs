namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IBus
    {
        event DomainEventsPublishedAsyncEventHandler Published;

        event DomainEventsPublishingAsyncEventHandler Publishing;

        Task PublishAsync(DomainEvent @event, CancellationToken? cancellationToken = default);

        Task PublishAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default);
    }
}