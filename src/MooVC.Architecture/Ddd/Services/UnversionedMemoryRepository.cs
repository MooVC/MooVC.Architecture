namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using MooVC.Serialization;

    public class UnversionedMemoryRepository<TAggregate>
        : MemoryRepository<TAggregate, Dictionary<Reference<TAggregate>, TAggregate>>
        where TAggregate : AggregateRoot
    {
        public UnversionedMemoryRepository(ICloner? cloner = default)
            : base(cloner: cloner)
        {
        }

        protected override Reference<TAggregate> GetKey(Guid id, SignedVersion? version)
        {
            return id.ToReference<TAggregate>();
        }

        protected override IEnumerable<TAggregate> PerformGetAll()
        {
            return Store.Values;
        }

        protected override Reference<TAggregate>? PerformGetCurrentVersion(TAggregate aggregate)
        {
            Reference<TAggregate>? key = GetKey(aggregate);

            if (Store.TryGetValue(key, out TAggregate? value))
            {
                return value.ToReference();
            }

            return default;
        }
    }
}