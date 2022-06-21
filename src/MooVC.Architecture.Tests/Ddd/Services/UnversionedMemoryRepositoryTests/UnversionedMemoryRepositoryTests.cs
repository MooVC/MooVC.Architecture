namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests;

using MooVC.Architecture.Serialization;
using MooVC.Serialization;

public abstract class UnversionedMemoryRepositoryTests
{
    protected UnversionedMemoryRepositoryTests()
    {
        Cloner = new TestableCloner();
    }

    public ICloner Cloner { get; }

    protected IRepository<TAggregate> Create<TAggregate>()
        where TAggregate : AggregateRoot
    {
        return Create<TAggregate>(Cloner);
    }

    protected virtual IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
       where TAggregate : AggregateRoot
    {
        return new UnversionedMemoryRepository<TAggregate>(cloner);
    }
}