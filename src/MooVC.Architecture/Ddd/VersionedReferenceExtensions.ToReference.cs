namespace MooVC.Architecture.Ddd
{
    public static partial class VersionedReferenceExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this VersionedReference<TAggregate> reference)
            where TAggregate : AggregateRoot
        {
            return new Reference<TAggregate>(reference.Id);
        }
    }
}