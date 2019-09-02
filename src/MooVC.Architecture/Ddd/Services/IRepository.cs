namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        event AggregateSavedEventHandler<TAggregate> AggregateSaved;

        event AggregateSavingEventHandler<TAggregate> AggregateSaving;

        TAggregate Get(Guid id, ulong? version = default);

        IEnumerable<TAggregate> GetAll();

        void Save(TAggregate aggregate);
    }
}