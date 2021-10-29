namespace MooVC.Architecture.Ddd
{
    using static MooVC.Architecture.Ddd.Ensure;

    public static partial class ReferenceExtensions
    {
        public static Reference<TAggregate> ToTyped<TAggregate>(this Reference? reference)
            where TAggregate : AggregateRoot
        {
            return ReferenceIsOfType<TAggregate>(reference, nameof(reference));
        }
    }
}