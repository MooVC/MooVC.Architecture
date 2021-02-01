namespace MooVC.Architecture.Serialization
{
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static VersionedReference<TAggregate> TryGetInternalVersionedReference<TAggregate>(
            this SerializationInfo info,
            string name)
            where TAggregate : AggregateRoot
        {
            return info.TryGetInternalValue(name, defaultValue: VersionedReference<TAggregate>.Empty);
        }
    }
}