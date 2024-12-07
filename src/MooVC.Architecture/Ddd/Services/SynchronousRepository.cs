namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Collections.Generic;
using MooVC.Diagnostics;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public abstract class SynchronousRepository<TAggregate>
    : Repository<TAggregate>
    where TAggregate : AggregateRoot
{
    protected SynchronousRepository(ICloner cloner, IDiagnosticsProxy? diagnostics = default)
        : base(diagnostics: diagnostics)
    {
        Cloner = IsNotNull(cloner, message: SynchronousRepositoryClonerRequired);
    }

    protected ICloner Cloner { get; }

    public override Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken? cancellationToken = default)
    {
        IEnumerable<TAggregate> aggregates = PerformGetAll();

        return aggregates.ProcessAllAsync(aggregate =>
            Cloner.CloneAsync(aggregate, cancellationToken: cancellationToken));
    }

    public override async Task<TAggregate?> GetAsync(Guid id, CancellationToken? cancellationToken = default, Sequence? version = default)
    {
        TAggregate? aggregate = PerformGet(id, version: version);

        if (aggregate is { })
        {
            return await Cloner
                .CloneAsync(aggregate, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        return default;
    }

    protected override Task<Reference<TAggregate>?> GetCurrentVersionAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        return Task.FromResult(PerformGetCurrentVersion(aggregate));
    }

    protected override async Task UpdateStoreAsync(TAggregate aggregate, CancellationToken? cancellationToken = default)
    {
        TAggregate clone = await Cloner
            .CloneAsync(aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        PerformUpdateStore(clone);
    }

    protected abstract IEnumerable<TAggregate> PerformGetAll();

    protected abstract TAggregate? PerformGet(Guid id, Sequence? version = default);

    protected abstract Reference<TAggregate>? PerformGetCurrentVersion(TAggregate aggregate);

    protected abstract void PerformUpdateStore(TAggregate aggregate);
}