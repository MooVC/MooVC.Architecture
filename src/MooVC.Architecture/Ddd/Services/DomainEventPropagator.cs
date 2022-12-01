namespace MooVC.Architecture.Ddd.Services;

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
        this.bus = IsNotNull(bus, message: DomainEventPropagatorBusRequired);
        this.reconciler = IsNotNull(reconciler, message: DomainEventPropagatorReconcilerRequired);

        this.reconciler.Reconciled += Reconciler_Reconciled;
    }

    private Task Reconciler_Reconciled(IAggregateReconciler sender, AggregateReconciledAsyncEventArgs e)
    {
        return bus.PublishAsync(e.Events, cancellationToken: e.CancellationToken);
    }
}