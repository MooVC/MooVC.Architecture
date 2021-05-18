namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAggregateReconciliationProxy
    {
        Task<EventCentricAggregateRoot?> GetAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default);

        Task<IEnumerable<EventCentricAggregateRoot>> GetAllAsync(
            CancellationToken? cancellationToken = default);

        Task<EventCentricAggregateRoot> CreateAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default);

        Task PurgeAsync(
            Reference aggregate,
            CancellationToken? cancellationToken = default);

        Task SaveAsync(
            EventCentricAggregateRoot aggregate,
            CancellationToken? cancellationToken = default);

        Task OverwriteAsync(
            EventCentricAggregateRoot aggregate,
            CancellationToken? cancellationToken = default);
    }
}