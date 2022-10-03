namespace MooVC.Architecture.Ddd;

using static MooVC.Architecture.Ddd.Ensure;

public static partial class ReferenceExtensions
{
    public static Reference<TAggregate> ToTyped<TAggregate>(this Reference? reference, bool unversioned = false)
        where TAggregate : AggregateRoot
    {
        return IsOfType<TAggregate>(reference, unversioned: unversioned);
    }
}