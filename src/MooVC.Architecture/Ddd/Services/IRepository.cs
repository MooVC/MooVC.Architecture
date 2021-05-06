namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        event AggregateSavedEventHandler<TAggregate> AggregateSaved;

        event AggregateSavingEventHandler<TAggregate> AggregateSaving;

        Task<IEnumerable<TAggregate>> GetAllAsync();

        Task<TAggregate?> GetAsync(Guid id, SignedVersion? version = default);

        Task SaveAsync(TAggregate aggregate);
    }
}