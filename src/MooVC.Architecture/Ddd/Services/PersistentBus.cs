namespace MooVC.Architecture.Ddd.Services
{
    using System;
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

        public PersistentBus(IStore<AtomicUnit, Guid> store)
        {
            ArgumentNotNull(store, nameof(store), PersistentBusStoreRequired);

            this.store = store;
        }

        protected override async Task PerformPublishAsync(params DomainEvent[] events)
        {
            var unit = new AtomicUnit(events);

            await PerformPersistAsync(unit)
                .ConfigureAwait(false);
        }

        private async Task PerformPersistAsync(AtomicUnit unit)
        {
            try
            {
                _ = await store
                    .CreateAsync(unit)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await
                    OnDiagnosticsEmittedAsync(
                        Level.Error,
                        cause: ex,
                        message: Format(PersistentBusPublishFailure, unit.Id))
                    .ConfigureAwait(false);

                throw;
            }
        }
    }
}