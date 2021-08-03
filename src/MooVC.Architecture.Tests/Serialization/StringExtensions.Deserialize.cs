namespace MooVC.Architecture.Serialization
{
    using Newtonsoft.Json;

    internal static partial class EnumerableExtensions
    {
        public static T Deserialize<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data, Settings.Default)!;
        }
    }
}