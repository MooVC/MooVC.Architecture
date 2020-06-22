namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    public interface ISnapshotProvider
    {
        ISnapshot? Generate(ulong? target = default);
    }
}