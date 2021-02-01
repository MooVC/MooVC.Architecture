namespace MooVC.Architecture.Serialization
{
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    public static class SerializableExtensions
    {
        public static T Clone<T>(this T original)
            where T : ISerializable
        {
            var cloner = new BinaryFormatterCloner();

            return cloner.Clone(original);
        }
    }
}