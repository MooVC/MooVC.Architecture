namespace MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepositoryTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenGetIsCalled
        : ConcurrentMemoryRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenAnExistingEntryExistsThenTheEntryIsReturned(bool useCloner)
        {
            var expected = new SerializableAggregateRoot();
            var other = new SerializableAggregateRoot();

            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            repository.Save(expected);
            repository.Save(other);

            SerializableAggregateRoot? actual = repository.Get(expected.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual!.Id);
            Assert.Equal(expected.Version, actual.Version);
            Assert.NotSame(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenNoExistingEntryExistsThenTheNullIsReturned(bool useCloner)
        {
            var other = new SerializableAggregateRoot();

            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            repository.Save(other);

            SerializableAggregateRoot? actual = repository.Get(Guid.NewGuid());

            Assert.Null(actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAnIdWhenTwoExistingVersionedEntriesExistThenTheMostUpToDateEntryIsReturned(bool useCloner)
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var other = new SerializableEventCentricAggregateRoot();

            ConcurrentMemoryRepository<SerializableEventCentricAggregateRoot> repository =
                Create<SerializableEventCentricAggregateRoot>(useCloner);

            repository.Save(aggregate);

            var context = new SerializableMessage();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            repository.Save(aggregate);
            repository.Save(other);

            SerializableEventCentricAggregateRoot? actual = repository.Get(aggregate.Id);

            Assert.NotSame(aggregate, actual);
            Assert.Equal(aggregate.Id, actual!.Id);
            Assert.Equal(aggregate.Version, actual.Version);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAVersionWhenTwoVersionedEntriesExistThenTheMatchingVersionedEntryIsReturned(bool useCloner)
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            SignedVersion expectedFirst = aggregate.Version;

            ConcurrentMemoryRepository<SerializableEventCentricAggregateRoot> repository =
                Create<SerializableEventCentricAggregateRoot>(useCloner);

            repository.Save(aggregate);

            var context = new SerializableMessage();

            aggregate.Set(new SetRequest(context, Guid.NewGuid()));

            SignedVersion expectedSecond = aggregate.Version;

            repository.Save(aggregate);

            var other = new SerializableEventCentricAggregateRoot();

            repository.Save(other);

            SerializableEventCentricAggregateRoot? actualFirst = repository.Get(aggregate.Id, version: expectedFirst);
            SerializableEventCentricAggregateRoot? actualSecond = repository.Get(aggregate.Id, version: expectedSecond);

            Assert.NotNull(actualFirst);
            Assert.NotSame(expectedFirst, actualFirst);
            Assert.Equal(aggregate.Id, actualFirst!.Id);
            Assert.Equal(expectedFirst, actualFirst.Version);

            Assert.NotNull(actualSecond);
            Assert.NotSame(expectedSecond, actualSecond);
            Assert.Equal(aggregate.Id, actualSecond!.Id);
            Assert.Equal(expectedSecond, actualSecond.Version);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GivenAVersionWhenNoExistingVersionedEntryMatchesThenNullIsReturned(bool useCloner)
        {
            var aggregate = new SerializableAggregateRoot();
            var other = new SerializableAggregateRoot();

            ConcurrentMemoryRepository<SerializableAggregateRoot> repository =
                Create<SerializableAggregateRoot>(useCloner);

            repository.Save(aggregate);
            repository.Save(other);

            SerializableAggregateRoot? actual = repository.Get(aggregate.Id, version: other.Version);

            Assert.Null(actual);
        }
    }
}