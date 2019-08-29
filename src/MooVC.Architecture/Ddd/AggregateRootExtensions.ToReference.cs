namespace MooVC.Architecture.Ddd
{
    public static partial class AggregateRootExtensions
    {
        public static Reference ToReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            return new Reference<TAggregate>(aggregate);
        }
    }
}