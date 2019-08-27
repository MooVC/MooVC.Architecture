namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Serialization;

    public sealed class MemoryRepository<TAggregate>
        : Repository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly IEnumerable<TAggregate> store;
        private readonly Action<TAggregate> insert;

        public MemoryRepository(bool isThreadSafe = false)
        {
            if (isThreadSafe)
            {
                var actual = new ConcurrentBag<TAggregate>();

                insert = value => actual.Add(value);
                store = actual;
            }
            else
            {
                var actual = new List<TAggregate>();

                insert = value => actual.Add(value);
                store = actual;
            }
        }

        public override TAggregate Get(Guid id, ulong? version = null)
        {
            return store
                .Where(aggregate => aggregate.Id == id 
                    && aggregate.Version == version.GetValueOrDefault(aggregate.Version))
                .OrderByDescending(aggregate => aggregate.Version)
                .FirstOrDefault();
        }

        public override IEnumerable<TAggregate> GetAll()
        {
            return store
                .GroupBy(aggregate => aggregate.Id)
                .Select(aggregate => aggregate.OrderByDescending(version => version.Version).First())
                .ToArray();
        }

        protected override ulong GetCurrentVersion(Guid id)
        {
            return Get(id)?.Version ?? ulong.MinValue;
        }

        protected override void PerformSave(TAggregate aggregate)
        {
            TAggregate copy = aggregate.Clone();
            
            aggregate.MarkChangesAsCommitted();

            insert(copy);
        }
    }
}