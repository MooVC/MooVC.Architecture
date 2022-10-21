namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    event AggregateSavingAbortedAsyncEventHandler<TAggregate> Aborted;

    event AggregateSavedAsyncEventHandler<TAggregate> Saved;

    event AggregateSavingAsyncEventHandler<TAggregate> Saving;

    Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken? cancellationToken = default);

    Task<TAggregate?> GetAsync(Guid id, CancellationToken? cancellationToken = default, SignedVersion? version = default);

    Task SaveAsync(TAggregate aggregate, CancellationToken? cancellationToken = default);
}