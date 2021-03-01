namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests
{
    using MooVC.Serialization;

    public abstract class UnversionedMemoryRepositoryTests
    {
        public UnversionedMemoryRepositoryTests()
        {
            Cloner = new BinaryFormatterCloner();
        }

        public ICloner Cloner { get; }

        protected IRepository<TAggregate> Create<TAggregate>(bool useCloner)
            where TAggregate : AggregateRoot
        {
            ICloner? cloner = useCloner
                ? Cloner
                : default;

            return Create<TAggregate>(cloner);
        }

        protected virtual IRepository<TAggregate> Create<TAggregate>(ICloner? cloner)
           where TAggregate : AggregateRoot
        {
            return new UnversionedMemoryRepository<TAggregate>(cloner: cloner);
        }
    }
}