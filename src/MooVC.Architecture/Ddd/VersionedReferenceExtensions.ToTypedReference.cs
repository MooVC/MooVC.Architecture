namespace MooVC.Architecture.Ddd
{
    using static Ensure;

    public static partial class VersionedReferenceExtensions
    {
        public static VersionedReference<TAggregate> ToTypedReference<TAggregate>(this VersionedReference reference)
            where TAggregate : AggregateRoot
        {
            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return (VersionedReference<TAggregate>)reference;
        }
    }
}