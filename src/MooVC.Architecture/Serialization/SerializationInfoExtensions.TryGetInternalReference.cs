namespace MooVC.Architecture.Serialization
{
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static Reference<TAggregate> TryGetInternalReference<TAggregate>(
            this SerializationInfo info,
            string name)
            where TAggregate : AggregateRoot
        {
            return info.TryGetInternalValue(name, defaultValue: Reference<TAggregate>.Empty);
        }
    }
}