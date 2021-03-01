namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class AggregateRootExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsToReferenceAggregateRequired);

            return new Reference<TAggregate>(aggregate);
        }
    }
}