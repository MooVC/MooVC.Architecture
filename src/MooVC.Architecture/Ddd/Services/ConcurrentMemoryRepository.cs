namespace MooVC.Architecture.Ddd.Services
{
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Ensure;

    public abstract class ConcurrentMemoryRepository<TAggregate>
        : MemoryRepository<TAggregate, ConcurrentDictionary<Reference<TAggregate>, TAggregate>>
        where TAggregate : AggregateRoot
    {
        protected ConcurrentMemoryRepository(ICloner cloner)
            : base(cloner)
        {
        }

        protected virtual TAggregate PerformAdd(Reference<TAggregate> key, TAggregate proposed)
        {
            AggregateDoesNotConflict(proposed);

            return proposed;
        }

        protected override Task PerformSaveAsync(
            TAggregate aggregate,
            CancellationToken? cancellationToken = default)
        {
            return UpdateStoreAsync(
                aggregate,
                cancellationToken: cancellationToken);
        }

        protected virtual TAggregate PerformUpdate(
            TAggregate existing,
            Reference<TAggregate> key,
            TAggregate proposed)
        {
            AggregateDoesNotConflict(proposed, currentVersion: existing.Version);

            return proposed;
        }

        protected override void PerformUpdateStore(TAggregate aggregate)
        {
            aggregate.MarkChangesAsCommitted();

            Reference<TAggregate> key = GetKey(aggregate);

            _ = Store.AddOrUpdate(
                key,
                _ => PerformAdd(key, aggregate),
                (_, existing) => PerformUpdate(existing, key, aggregate));
        }
    }
}