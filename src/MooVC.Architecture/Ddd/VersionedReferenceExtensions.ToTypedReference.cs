namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Ensure;

    public static partial class VersionedReferenceExtensions
    {
        public static VersionedReference<TAggregate> ToTypedReference<TAggregate>(this VersionedReference reference)
            where TAggregate : AggregateRoot
        {
            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return reference is VersionedReference<TAggregate> response
                ? response
                : new VersionedReference<TAggregate>(reference.Id, reference.Version);
        }
    }
}