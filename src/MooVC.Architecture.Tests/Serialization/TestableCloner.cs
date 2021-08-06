namespace MooVC.Architecture.Serialization
{
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Serialization;

    public sealed class TestableCloner
        : ICloner
    {
        public Task<T> CloneAsync<T>(
            T original,
            CancellationToken? cancellationToken = default)
            where T : notnull
        {
            T clone = original
                .Serialize()
                .Deserialize<T>();

            return Task.FromResult(clone);
        }
    }
}