namespace MooVC.Architecture.Ddd.Services;

using System.Threading.Tasks;

public delegate Task AggregateSavingAbortedAsyncEventHandler<TAggregate>(
    IRepository<TAggregate> sender,
    AggregateSavingAbortedAsyncEventArgs<TAggregate> e)
    where TAggregate : AggregateRoot;