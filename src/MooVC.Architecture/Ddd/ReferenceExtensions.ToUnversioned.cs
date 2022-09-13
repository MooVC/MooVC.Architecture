namespace MooVC.Architecture.Ddd;

using System.Diagnostics.CodeAnalysis;
using static MooVC.Architecture.Ddd.Reference;

public static partial class ReferenceExtensions
{
    [return: NotNullIfNotNull("reference")]
    public static Reference? ToUnversioned(this Reference? reference)
    {
        if (reference is { } && reference.IsVersioned)
        {
            return Create(reference.Id, reference.Type);
        }

        return reference;
    }

    [return: NotNullIfNotNull("reference")]
    public static Reference<TAggregate>? ToUnversioned<TAggregate>(this Reference<TAggregate>? reference)
        where TAggregate : AggregateRoot
    {
        if (reference is { } && reference.IsVersioned)
        {
            return new Reference<TAggregate>(reference.Id);
        }

        return reference;
    }
}