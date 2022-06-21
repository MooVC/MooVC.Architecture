namespace MooVC.Architecture.Ddd.Services.VersionedConcurrentMemoryRepositoryTests;

using MooVC.Serialization;
using Base = MooVC.Architecture.Ddd.Services.VersionedMemoryRepositoryTests.WhenGetAsyncIsCalled;

public sealed class WhenGetAsyncIsCalled
    : Base
{
    protected override IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
    {
        return new VersionedConcurrentMemoryRepository<TAggregate>(cloner);
    }
}