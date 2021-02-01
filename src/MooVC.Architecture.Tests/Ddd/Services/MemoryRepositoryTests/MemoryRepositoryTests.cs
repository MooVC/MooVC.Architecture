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
    }
}