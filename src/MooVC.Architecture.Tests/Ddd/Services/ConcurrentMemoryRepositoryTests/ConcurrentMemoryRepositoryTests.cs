namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using MooVC.Serialization;

    public abstract class ConcurrentMemoryRepositoryTests
    {
        protected ConcurrentMemoryRepositoryTests()
        {
            Cloner = new BinaryFormatterCloner();
        }

        public ICloner Cloner { get; }

        protected ConcurrentMemoryRepository<TAggregate> Create<TAggregate>(bool useCloner)
            where TAggregate : AggregateRoot
        {
            ICloner? cloner = useCloner
                ? Cloner
                : default;

            return new ConcurrentMemoryRepository<TAggregate>(cloner: cloner);
        }
    }
}