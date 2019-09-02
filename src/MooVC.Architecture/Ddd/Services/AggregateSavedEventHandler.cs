namespace MooVC.Architecture.Ddd.Services
{
    public delegate void AggregateSavedEventHandler<TAggregate>(IRepository<TAggregate> sender, AggregateSavedEventArgs<TAggregate> e)
        where TAggregate : AggregateRoot;
}