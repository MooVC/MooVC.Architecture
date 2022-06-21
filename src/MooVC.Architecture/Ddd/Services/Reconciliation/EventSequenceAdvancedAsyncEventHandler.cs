namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task EventSequenceAdvancedAsyncEventHandler(IEventReconciler sender, EventSequenceAdvancedAsyncEventArgs e);