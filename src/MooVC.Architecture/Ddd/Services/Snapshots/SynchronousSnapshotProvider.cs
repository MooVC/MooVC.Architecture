namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Threading.Tasks;

    public abstract class SynchronousSnapshotProvider
        : ISnapshotProvider
    {
        public virtual Task<ISnapshot?> GenerateAsync(ulong? target = default)
        {
            return Task.FromResult(PerformGenerate(target: target));
        }

        protected abstract ISnapshot? PerformGenerate(ulong? target);
    }
}