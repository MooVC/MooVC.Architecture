namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Reference;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class ReferenceExtensions
    {
        public static Reference ToUnversioned(this Reference reference)
        {
            ArgumentNotNull(
                reference,
                nameof(reference),
                ReferenceExtensionsToUnversionedReferenceRequired);

            if (reference.IsVersioned)
            {
                return Create(reference.Type, reference.Id);
            }

            return reference;
        }

        public static Reference<TAggregate> ToUnversioned<TAggregate>(this Reference<TAggregate> reference)
            where TAggregate : AggregateRoot
        {
            ArgumentNotNull(
                reference,
                nameof(reference),
                ReferenceExtensionsToUnversionedReferenceRequired);

            if (reference.IsVersioned)
            {
                return new Reference<TAggregate>(reference.Id);
            }

            return reference;
        }
    }
}