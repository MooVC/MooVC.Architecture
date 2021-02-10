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
        : Bus,
          IEmitDiagnostics
    {
        private readonly IStore<AtomicUnit, Guid> store;

        public PersistentBus(IStore<AtomicUnit, Guid> store)
        {
            ArgumentNotNull(store, nameof(store), PersistentBusStoreRequired);

            this.store = store;
        }

        public event DiagnosticsEmittedEventHandler? DiagnosticsEmitted;

        // TODO: Change from async VOID

        protected override async void PerformPublish(DomainEvent[] events)
        {
            var unit = new AtomicUnit(events);

            async Task PersistAsync()
            {
                await PerformPersistAsync(unit)
                    .ConfigureAwait(false);
            }

            try
            {
                await PersistAsync();
            }
            catch
            {
                OnUnhandled(() => PersistAsync(), events);
            }
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
                OnDiagnosticsEmitted(
                    Level.Error,
                    cause: ex,
                    message: Format(PersistentBusPublishFailure, unit.Id));

                throw;
            }
        }

        private void OnDiagnosticsEmitted(Level level, Exception? cause = default, string? message = default)
        {
            DiagnosticsEmitted?.Invoke(
                this,
                new DiagnosticsEmittedEventArgs(
                    cause: cause,
                    level: level,
                    message: message));
        }
    }
}