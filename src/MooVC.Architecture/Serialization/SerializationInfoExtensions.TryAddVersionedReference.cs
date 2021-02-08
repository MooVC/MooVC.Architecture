namespace MooVC.Architecture.Serialization
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static bool TryAddVersionedReference(
            this SerializationInfo info,
            string name,
            [NotNullWhen(true)] VersionedReference? reference)
        {
            return info.TryAddValue(name, reference, predicate: _ => reference is { } && !reference.IsEmpty);
        }
    }
}