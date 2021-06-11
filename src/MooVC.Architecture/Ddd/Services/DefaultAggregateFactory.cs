namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Linq;

    public abstract class DefaultAggregateFactory
        : IAggregateFactory
    {
        public abstract Task<EventCentricAggregateRoot> CreateAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default);

        public async Task<EventCentricAggregateRoot> CreateAsync(
            IEnumerable<DomainEvent> events,
            CancellationToken? cancellationToken = default)
        {
            if (!events.SafeAny())
            {
                throw new DomainEventsMissingException();
            }

            Reference aggregate = events.First().Aggregate;

            EventCentricAggregateRoot instance = await
                CreateAsync(
                    aggregate,
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            instance.LoadFromHistory(events);

            return instance;
        }
    }
}