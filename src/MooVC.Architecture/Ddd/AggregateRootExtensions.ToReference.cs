namespace MooVC.Architecture.Ddd;

using Ardalis.GuardClauses;
using static MooVC.Architecture.Ddd.Resources;

public static partial class AggregateRootExtensions
{
    public static Reference<TAggregate> ToReference<TAggregate>(this TAggregate aggregate, bool unversioned = false)
        where TAggregate : AggregateRoot
    {
        _ = Guard.Against.Null(aggregate, message: AggregateRootExtensionsToReferenceAggregateRequired);

        if (unversioned)
        {
            return new Reference<TAggregate>(aggregate.Id, aggregate.GetType());
        }

        return new Reference<TAggregate>(aggregate);
    }
}