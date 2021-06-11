namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAggregateFactory
    {
        Task<EventCentricAggregateRoot> CreateAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default);

        Task<EventCentricAggregateRoot> CreateAsync(
            IEnumerable<DomainEvent> events,
            CancellationToken? cancellationToken = default);
    }
}