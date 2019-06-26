namespace MooVC.Architecture.Ddd.Services
{
    using System;

    public interface IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        TAggregate Get(Guid id, ulong? version = null);

        void Save(TAggregate aggregate);
    }
}