namespace MooVC.Architecture
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static partial class ObjectExtensions
    {
        public static T Serialize<T>(this T original)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, original);
                _ = stream.Seek(0, SeekOrigin.Begin);

                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }
}