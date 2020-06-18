namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Logging;
    using MooVC.Persistence;
    using static System.String;
    using static MooVC.Ensure;
    using static Resources;

    public sealed class PersistentBus
        : Bus,
          IEmitFailures
    {
        private readonly IStore<AtomicUnit, Guid> store;

        public PersistentBus(IStore<AtomicUnit, Guid> store)
        {
            ArgumentNotNull(store, nameof(store), PersistentBusStoreRequired);

            this.store = store;
        }

        public event PassiveExceptionEventHandler? FailureEmitted;

        protected override void PerformPublish(DomainEvent[] events)
        {
            var unit = new AtomicUnit(events);

            void Persist()
            {
                PerformPersist(unit);
            }

            try
            {
                Persist();
            }
            catch
            {
                OnUnhandled(() => Task.Run(Persist), events);
            }
        }

        private void PerformPersist(AtomicUnit unit)
        {
            try
            {
                _ = store.Create(unit);
            }
            catch (Exception ex)
            {
                OnFailureEncountered(Format(PersistentBusPublishFailure, unit.Id), ex);

                throw;
            }
        }

        private void OnFailureEncountered(string message, Exception failure)
        {
            FailureEmitted?.Invoke(
                this,
                new PassiveExceptionEventArgs(message, exception: failure));
        }
    }
}