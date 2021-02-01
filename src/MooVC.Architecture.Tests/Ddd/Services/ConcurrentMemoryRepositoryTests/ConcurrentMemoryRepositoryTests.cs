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
    }
}