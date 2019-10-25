namespace MooVC.Architecture.Ddd.Services
{
    public delegate void AggregateSavingEventHandler<TAggregate>(IRepository<TAggregate> sender, AggregateSavingEventArgs<TAggregate> e)
        where TAggregate : AggregateRoot;
}