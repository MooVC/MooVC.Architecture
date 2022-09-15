namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Diagnostics;
using MooVC.Persistence;
using static System.String;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

public sealed class PersistentBus
    : Bus
{
    private readonly IStore<AtomicUnit, Guid> store;

    public PersistentBus(IStore<AtomicUnit, Guid> store, IDiagnosticsProxy? diagnostics = default)
        : base(diagnostics: diagnostics)
    {
        this.store = ArgumentNotNull(store, nameof(store), PersistentBusStoreRequired);
    }

    protected override async Task PerformPublishAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        var unit = new AtomicUnit(events);

        await PerformPersistAsync(unit, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task PerformPersistAsync(AtomicUnit unit, CancellationToken? cancellationToken = default)
    {
        try
        {
            _ = await store
                .CreateAsync(unit, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await
                OnDiagnosticsEmittedAsync(
                    cancellationToken: cancellationToken,
                    cause: ex,
                    message: Format(PersistentBusPublishFailure, unit.Id))
                .ConfigureAwait(false);

            throw;
        }
    }
}