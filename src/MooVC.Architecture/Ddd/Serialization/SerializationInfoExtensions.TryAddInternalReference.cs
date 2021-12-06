namespace MooVC.Architecture.Ddd.Serialization
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static bool TryAddInternalReference(
            this SerializationInfo info,
            string name,
            [NotNullWhen(true)] Reference? reference)
        {
            return info.TryAddInternalValue(
                name,
                reference,
                predicate: _ => reference is { });
        }

        public static bool TryAddInternalReference<TAggregate>(
            this SerializationInfo info,
            string name,
            [NotNullWhen(true)] Reference<TAggregate>? reference)
            where TAggregate : AggregateRoot
        {
            return info.TryAddInternalValue(
                name,
                reference,
                predicate: _ => reference is { } && !reference.IsEmpty);
        }
    }
}