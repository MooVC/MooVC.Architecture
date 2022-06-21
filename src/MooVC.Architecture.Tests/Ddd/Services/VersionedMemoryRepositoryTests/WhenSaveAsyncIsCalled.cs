namespace MooVC.Architecture.Ddd.Services.VersionedMemoryRepositoryTests;

using MooVC.Serialization;
using Base = MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests.WhenSaveAsyncIsCalled;

public class WhenSaveAsyncIsCalled
    : Base
{
    protected override IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
    {
        return new VersionedMemoryRepository<TAggregate>(cloner);
    }
}