namespace MooVC.Architecture.Ddd.Services.Snapshots;

using System.Threading;
using System.Threading.Tasks;

public interface ISnapshotProvider
{
    Task<ISnapshot?> GenerateAsync(CancellationToken? cancellationToken = default, ulong? target = default);
}