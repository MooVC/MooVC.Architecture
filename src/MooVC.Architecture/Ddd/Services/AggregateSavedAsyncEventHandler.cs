namespace MooVC.Architecture.Ddd.Services;

using System.Threading.Tasks;

public delegate Task AggregateSavedAsyncEventHandler<TAggregate>(IRepository<TAggregate> sender, AggregateSavedAsyncEventArgs<TAggregate> e)
    where TAggregate : AggregateRoot;