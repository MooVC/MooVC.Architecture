namespace MooVC.Architecture.Ddd
{
    using static Ensure;

    public static partial class ReferenceExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this Reference reference)
            where TAggregate : AggregateRoot
        {
            ReferenceIsOfType<TAggregate>(reference, nameof(reference));

            return (Reference<TAggregate>)reference;
        }
    }
}