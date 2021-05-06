namespace MooVC.Architecture.Ddd.Services.UnversionedConcurrentMemoryRepositoryTests
{
    using MooVC.Serialization;
    using Base = MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests.WhenSaveAsyncIsCalled;

    public sealed class WhenSaveAsyncIsCalled
        : Base
    {
        protected override IRepository<TAggregate> Create<TAggregate>(ICloner? cloner)
        {
            return new UnversionedConcurrentMemoryRepository<TAggregate>(cloner: cloner);
        }
    }
}