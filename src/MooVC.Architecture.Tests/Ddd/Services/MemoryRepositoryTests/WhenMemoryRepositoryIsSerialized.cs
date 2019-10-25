namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenMemoryRepositoryIsSerialized
    {
        [Fact]
        public void GivenAnEmptyInstanceThenNoEntriesAreSerialized()
        {
            var repository = new MemoryRepository<AggregateRoot>();
            MemoryRepository<AggregateRoot> clone = repository.Clone();

            Assert.NotSame(repository, clone);
            Assert.Equal(repository.GetAll(), clone.GetAll());
        }

        [Fact]
        public void GivenANonEmptyInstanceThenAllEntriesAreSerialized()
        {
            const int ExpectedTotal = 2;

            var firstAggregate = new SerializableAggregateRoot();
            var secondAggregate = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>();

            repository.Save(firstAggregate);
            repository.Save(secondAggregate);

            MemoryRepository<SerializableAggregateRoot> clone = repository.Clone();

            Assert.NotSame(repository, clone);
            Assert.Equal(ExpectedTotal, clone.GetAll().Count());
            Assert.Equal(repository.GetAll(), clone.GetAll());
        }
    }
}