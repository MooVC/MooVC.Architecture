namespace MooVC.Architecture.Ddd.Services.VersionedConcurrentMemoryRepositoryTests
{
    using MooVC.Serialization;
    using Base = MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests.WhenSaveAsyncIsCalled;

    public class WhenSaveAsyncIsCalled
        : Base
    {
        protected override IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
        {
            return new VersionedConcurrentMemoryRepository<TAggregate>(cloner);
        }
    }
}