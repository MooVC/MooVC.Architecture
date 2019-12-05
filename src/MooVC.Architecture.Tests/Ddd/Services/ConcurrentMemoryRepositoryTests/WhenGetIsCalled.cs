namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenGetIsCalled
    {
        [Fact]
        public void GivenAnIdWhenAnExistingEntryExistsThenTheEntryIsReturned()
        {
            const ulong ExpectedVersion = 1;

            var expected = new SerializableAggregateRoot();
            var other = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(expected);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(expected.Id);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(ExpectedVersion, actual.Version);
            Assert.NotSame(expected, actual);
        }

        [Fact]
        public void GivenAnIdWhenNoExistingEntryExistsThenTheNullIsReturned()
        {
            var other = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(Guid.NewGuid());

            Assert.Null(actual);
        }

        [Fact]
        public void GivenAnIdWhenTwoExistingVersionedEntriesExistThenTheMostUpToDateEntryIsReturned()
        {
            const ulong ExpectedSecondVersion = 2,
                FirstVersion = 1;

            var id = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(id, version: FirstVersion);
            var expected = new SerializableAggregateRoot(id, version: ExpectedSecondVersion);
            var other = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(aggregate);
            repository.Save(expected);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(id);

            Assert.NotSame(expected, actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(ExpectedSecondVersion, actual.Version);
        }

        [Fact]
        public void GivenAVersionWhenTwoVersionedEntriesExistThenTheMatchingVersionedEntryIsReturned()
        {
            const ulong ExpectedFirstVersion = 1,
                ExpectedSecondVersion = 2;

            var id = Guid.NewGuid();
            var expectedFirst = new SerializableAggregateRoot(id, version: ExpectedFirstVersion);
            var expectedSecond = new SerializableAggregateRoot(id, version: ExpectedSecondVersion);
            var other = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(expectedFirst);
            repository.Save(expectedSecond);
            repository.Save(other);

            SerializableAggregateRoot actualFirst = repository.Get(id, version: ExpectedFirstVersion);
            SerializableAggregateRoot actualSecond = repository.Get(id, version: ExpectedSecondVersion);

            Assert.NotSame(expectedFirst, actualFirst);
            Assert.Equal(expectedFirst.Id, actualFirst.Id);
            Assert.Equal(ExpectedFirstVersion, actualFirst.Version);

            Assert.NotSame(expectedSecond, actualSecond);
            Assert.Equal(expectedSecond.Id, actualSecond.Id);
            Assert.Equal(ExpectedSecondVersion, actualSecond.Version);
        }

        [Fact]
        public void GivenAVersionWhenNoExistingVersionedEntryMatchesThenNullIsReturned()
        {
            var id = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(id, version: 1);
            var other = new SerializableAggregateRoot();
            var repository = new ConcurrentMemoryRepository<SerializableAggregateRoot>();

            repository.Save(aggregate);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(id, version: 2);

            Assert.Null(actual);
        }
    }
}