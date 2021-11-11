namespace MooVC.Architecture.Ddd.Serialization
{
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static Reference TryGetInternalReference(
            this SerializationInfo info,
            string name,
            Reference? defaultValue = default)
        {
            defaultValue ??= Reference<AggregateRoot>.Empty;

            return info.TryGetInternalValue(name, defaultValue: defaultValue);
        }

        public static Reference<TAggregate> TryGetInternalReference<TAggregate>(
            this SerializationInfo info,
            string name)
            where TAggregate : AggregateRoot
        {
            return info.TryGetInternalValue(name, defaultValue: Reference<TAggregate>.Empty);
        }
    }
}