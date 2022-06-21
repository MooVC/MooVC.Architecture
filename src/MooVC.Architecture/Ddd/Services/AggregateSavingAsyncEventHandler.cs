namespace MooVC.Architecture.Ddd.Services;

using System.Threading.Tasks;

public delegate Task AggregateSavingAsyncEventHandler<TAggregate>(IRepository<TAggregate> sender, AggregateSavingAsyncEventArgs<TAggregate> e)
    where TAggregate : AggregateRoot;