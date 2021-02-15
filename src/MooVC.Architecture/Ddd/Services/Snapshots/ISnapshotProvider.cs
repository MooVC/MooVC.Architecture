namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Threading.Tasks;

    public interface ISnapshotProvider
    {
        Task<ISnapshot?> GenerateAsync(ulong? target = default);
    }
}