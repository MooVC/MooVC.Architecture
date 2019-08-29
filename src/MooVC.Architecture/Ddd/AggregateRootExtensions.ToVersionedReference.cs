namespace MooVC.Architecture.Ddd
{
    public static partial class AggregateRootExtensions
    {
        public static VersionedReference ToVersionedReference<TAggregate>(this TAggregate aggregate)
            where TAggregate : AggregateRoot
        {
            return new VersionedReference<TAggregate>(aggregate);
        }
    }
}