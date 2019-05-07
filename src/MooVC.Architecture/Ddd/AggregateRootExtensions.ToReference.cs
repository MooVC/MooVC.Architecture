namespace MooVC.Architecture.Ddd
{
    public static class AggregateRootExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            return new Reference<TAggregate>(aggregate.Id);
        }
    }
}