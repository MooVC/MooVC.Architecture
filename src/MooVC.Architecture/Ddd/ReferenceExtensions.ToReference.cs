namespace MooVC.Architecture.Ddd
{
    public static partial class ReferenceExtensions
    {
        public static Reference<TAggregate> ToReference<TAggregate>(this IReference reference)
            where TAggregate : AggregateRoot
        {
            if (typeof(TAggregate) != reference.Type)
            {
                throw new AggregateReferenceMismatchException<TAggregate>(reference);
            }

            return new Reference<TAggregate>(reference.Id, version: reference.Version);
        }
    }
}