namespace MooVC.Architecture.Ddd;

using System.Diagnostics.CodeAnalysis;

public static partial class ReferenceExtensions
{
    public static bool Is<TAggregate>(this Reference? reference, [NotNullWhen(true)] out Reference<TAggregate>? aggregate)
        where TAggregate : AggregateRoot
    {
        aggregate = reference as Reference<TAggregate>;

        if (aggregate is not null)
        {
            return true;
        }

        if (reference is null || !typeof(TAggregate).IsAssignableFrom(reference.Type))
        {
            return false;
        }

        aggregate = Reference<TAggregate>.Create(reference.Id, version: reference.Version);

        return true;
    }
}