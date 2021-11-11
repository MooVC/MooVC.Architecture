namespace MooVC.Architecture.Ddd.Serialization
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static bool TryAddReference(
            this SerializationInfo info,
            string name,
            [NotNullWhen(true)] Reference? reference)
        {
            return info.TryAddValue(
                name,
                reference,
                predicate: _ => reference is { });
        }

        public static bool TryAddReference<TAggregate>(
            this SerializationInfo info,
            string name,
            [NotNullWhen(true)] Reference<TAggregate>? reference)
            where TAggregate : AggregateRoot
        {
            return info.TryAddValue(
                name,
                reference,
                predicate: _ => reference is { } && !reference.IsEmpty);
        }
    }
}