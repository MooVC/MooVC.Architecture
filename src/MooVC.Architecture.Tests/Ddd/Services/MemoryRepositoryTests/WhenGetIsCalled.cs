namespace MooVC.Architecture.Ddd.Services.MemoryRepositoryTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenMemoryRepositoryIsConstructed
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenAnExistingEntryExistsThenTheEntryIsReturned(bool isThreadSafe)
        {
            const ulong ExpectedVersion = 1;

            var expected = new SerializableAggregateRoot();
            var other = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(expected);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(expected.Id);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(ExpectedVersion, actual.Version);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenNoExistingEntryExistsThenTheNullIsReturned(bool isThreadSafe)
        {
            var other = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(Guid.NewGuid());

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenTwoExistingVersionedEntriesExistThenTheMostUpToDateEntryIsReturned(bool isThreadSafe)
        {
            const ulong ExpectedSecondVersion = 2,
                FirstVersion = 1;

            var id = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(id, version: FirstVersion);
            var expected = new SerializableAggregateRoot(id, version: ExpectedSecondVersion);
            var other = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(aggregate);
            repository.Save(expected);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(id);

            Assert.NotSame(expected, actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(ExpectedSecondVersion, actual.Version);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAVersionWhenTwoVersionedEntriesExistThenTheMatchingVersionedEntryIsReturned(bool isThreadSafe)
        {
            const ulong ExpectedFirstVersion = 1,
                ExpectedSecondVersion = 2;

            var id = Guid.NewGuid();
            var expectedFirst = new SerializableAggregateRoot(id, version: ExpectedFirstVersion);
            var expectedSecond = new SerializableAggregateRoot(id, version: ExpectedSecondVersion);
            var other = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAVersionWhenNoExistingVersionedEntryMatchesThenNullIsReturned(bool isThreadSafe)
        {
            var id = Guid.NewGuid();
            var aggregate = new SerializableAggregateRoot(id, version: 1);
            var other = new SerializableAggregateRoot();
            var repository = new MemoryRepository<SerializableAggregateRoot>(isThreadSafe: isThreadSafe);

            repository.Save(aggregate);
            repository.Save(other);

            SerializableAggregateRoot actual = repository.Get(id, version: 2);

            Assert.Null(actual);
        }
    }
}