namespace MooVC.Architecture.Ddd.Services
{
    public sealed class AggregateSavedEventArgs<TAggregate>
        : AggregateEventArgs<TAggregate>
        where TAggregate : AggregateRoot
    {
        public AggregateSavedEventArgs(TAggregate aggregate)
            : base(aggregate)
        {
        }
    }
}