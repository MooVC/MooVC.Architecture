namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class AggregateRootExtensions
    {
        public static VersionedReference<TAggregate> ToVersionedReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsToVersionedReferenceAggregateRequired);

            return new VersionedReference<TAggregate>(aggregate);
        }
    }
}