namespace MooVC.Architecture.Ddd.Services
{
    public sealed class AggregateSavingEventArgs<TAggregate>
        : AggregateEventArgs<TAggregate>
        where TAggregate : AggregateRoot
    {
        public AggregateSavingEventArgs(TAggregate aggregate)
            : base(aggregate)
        {
        }
    }
}