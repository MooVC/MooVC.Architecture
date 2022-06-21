namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System.Threading.Tasks;

public delegate Task UnsupportedAggregateTypeDetectedAsyncEventHandler(
    object sender,
    UnsupportedAggregateTypeDetectedAsyncEventArgs e);