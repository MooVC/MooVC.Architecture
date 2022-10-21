namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task EventsReconciledAsyncEventHandler(IEventReconciler sender, EventsReconciliationAsyncEventArgs e);