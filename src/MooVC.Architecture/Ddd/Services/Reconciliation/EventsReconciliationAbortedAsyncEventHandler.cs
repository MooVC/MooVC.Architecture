namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task EventsReconciliationAbortedAsyncEventHandler(IEventReconciler sender, EventsReconciliationAbortedAsyncEventArgs e);