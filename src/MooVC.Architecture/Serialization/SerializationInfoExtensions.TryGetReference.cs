namespace MooVC.Architecture.Serialization
{
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;

    public static partial class SerializationInfoExtensions
    {
        public static Reference<TAggregate> TryGetReference<TAggregate>(this SerializationInfo info, string name)
            where TAggregate : AggregateRoot
        {
            return info.TryGetValue(name, defaultValue: Reference<TAggregate>.Empty);
        }
    }
}