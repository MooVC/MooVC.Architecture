namespace MooVC.Architecture.Ddd.Services.UnversionedConcurrentMemoryRepositoryTests
{
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Serialization;
    using Base = MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests.WhenGetAllAsyncIsCalled;

    public sealed class WhenGetAllAsyncIsCalled
        : Base
    {
        protected override IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
        {
            return new UnversionedConcurrentMemoryRepository<TAggregate>(cloner);
        }
    }
}