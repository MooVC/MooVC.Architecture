namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Threading.Tasks;

    public abstract class SynchronousSnapshotProvider
        : ISnapshotProvider
    {
        public virtual async Task<ISnapshot?> GenerateAsync(ulong? target = null)
        {
            return await Task.FromResult(PerformGenerate(target: target));
        }

        protected abstract ISnapshot? PerformGenerate(ulong? target);
    }
}