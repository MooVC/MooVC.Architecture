namespace MooVC.Architecture.Ddd
{
    public static class AggregateRootExtensions
    {
        public static IReference ToReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            return new Reference<TAggregate>(aggregate);
        }
    }
}