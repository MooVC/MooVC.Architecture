namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAggregateReconciliationProxy
    {
        Task<EventCentricAggregateRoot?> GetAsync(Reference aggregate);

        Task<IEnumerable<EventCentricAggregateRoot>> GetAllAsync();

        Task<EventCentricAggregateRoot> CreateAsync(Reference aggregate);

        Task PurgeAsync(Reference aggregate);

        Task SaveAsync(EventCentricAggregateRoot aggregate);

        Task OverwriteAsync(EventCentricAggregateRoot aggregate);
    }
}