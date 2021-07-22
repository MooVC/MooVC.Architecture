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
        protected ConcurrentMemoryRepository(ICloner? cloner = default)
            : base(cloner: cloner)
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
            PerformUpdateStore(aggregate);

            return Task.CompletedTask;
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
            TAggregate copy = Cloner(aggregate);

            copy.MarkChangesAsCommitted();

            Reference<TAggregate> key = GetKey(copy);

            _ = Store.AddOrUpdate(
                key,
                _ => PerformAdd(key, copy),
                (_, existing) => PerformUpdate(existing, key, copy));
        }
    }
}