namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class SynchronousSnapshotProvider
        : ISnapshotProvider
    {
        public virtual Task<ISnapshot?> GenerateAsync(
            CancellationToken? cancellationToken = default,
            ulong? target = default)
        {
            return Task.FromResult(PerformGenerate(target: target));
        }

        protected abstract ISnapshot? PerformGenerate(ulong? target);
    }
}