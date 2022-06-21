namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task EventsReconcilingAsyncEventHandler(IEventReconciler sender, EventReconciliationAsyncEventArgs e);