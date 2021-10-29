namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public sealed class DomainEventPropagator
    {
        private readonly IBus bus;
        private readonly IAggregateReconciler reconciler;

        public DomainEventPropagator(IBus bus, IAggregateReconciler reconciler)
        {
            this.bus = ArgumentNotNull(
                bus,
                nameof(bus),
                DomainEventPropagatorBusRequired);

            this.reconciler = ArgumentNotNull(
                reconciler,
                nameof(reconciler),
                DomainEventPropagatorReconcilerRequired);

            this.reconciler.AggregateReconciled += Reconciler_AggregateReconciled;
        }

        private async Task Reconciler_AggregateReconciled(
            IAggregateReconciler sender,
            AggregateReconciledAsyncEventArgs e)
        {
            await bus
                .PublishAsync(e.Events, cancellationToken: e.CancellationToken)
                .ConfigureAwait(false);
        }
    }
}