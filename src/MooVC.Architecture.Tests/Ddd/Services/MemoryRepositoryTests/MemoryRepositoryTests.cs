namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using MooVC.Serialization;

    public abstract class MemoryRepositoryTests
    {
        public MemoryRepositoryTests()
        {
            Cloner = new BinaryFormatterCloner();
        }

        public ICloner Cloner { get; }

        protected MemoryRepository<TAggregate> Create<TAggregate>(bool useCloner)
            where TAggregate : AggregateRoot
        {
            ICloner? cloner = useCloner
                ? Cloner
                : default;

            return new MemoryRepository<TAggregate>(cloner: cloner);
        }
    }
}