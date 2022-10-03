namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Collections.Generic;
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
        this.store = IsNotNull(store, message: PersistentBusStoreRequired);
    }

    protected override Task PerformPublishAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        var unit = new AtomicUnit(events);

        return PerformPersistAsync(unit, cancellationToken: cancellationToken);
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
            await Diagnostics
                .EmitAsync(
                    cancellationToken: cancellationToken,
                    cause: ex,
                    impact: Impact.Unrecoverable,
                    message: (PersistentBusPublishFailure, unit.Id))
                .ConfigureAwait(false);

            throw;
        }
    }
}