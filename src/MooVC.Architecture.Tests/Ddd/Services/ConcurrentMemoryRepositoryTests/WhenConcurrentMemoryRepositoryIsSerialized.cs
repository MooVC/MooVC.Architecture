namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenConcurrentMemoryRepositoryIsSerialized
    {
        [Fact]
        public void GivenAnEmptyInstanceThenNoEntriesAreSerialized()
        {
            var repository = new ConcurrentMemoryRepository<AggregateRoot>();
            ConcurrentMemoryRepository<AggregateRoot> clone = repository.Clone();

            Assert.NotSame(repository, clone);
            Assert.Equal(repository.GetAll(), clone.GetAll());
        }

        [Fact]
        public void GivenANonEmptyInstanceThenAllEntriesAreSerialized()
        {
            const int ExpectedTotal = 2;

            var firstAggregate = new SerializableAggregateRoot();
            var secondAggregate = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(firstAggregate);
            repository.Save(secondAggregate);

            ConcurrentMemoryRepository<SerializableAggregateRoot> clone = repository.Clone();

            Assert.NotSame(repository, clone);
            Assert.Equal(ExpectedTotal, clone.GetAll().Count());
            Assert.Equal(repository.GetAll(), clone.GetAll());
        }
    }
}