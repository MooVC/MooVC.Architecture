namespace MooVC.Architecture.Ddd.Services.VersionedMemoryRepositoryTests;

using System;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests;
using MooVC.Architecture.MessageTests;
using MooVC.Serialization;
using Xunit;

public class WhenGetAsyncIsCalled
    : UnversionedMemoryRepositoryTests
{
    [Fact]
    public async Task GivenAnIdWhenAnExistingEntryExistsThenTheEntryIsReturnedAsync()
    {
        var expected = new SerializableAggregateRoot();
        var other = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(expected);
        await repository.SaveAsync(other);

        SerializableAggregateRoot? actual = await repository.GetAsync(expected.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Id, actual!.Id);
        Assert.Equal(expected.Version, actual.Version);
        Assert.NotSame(expected, actual);
    }

    [Fact]
    public async Task GivenAnIdWhenNoExistingEntryExistsThenANullValueIsReturnedAsync()
    {
        var other = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(other);

        SerializableAggregateRoot? actual = await repository.GetAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task GivenAnIdWhenTwoExistingVersionedEntriesExistThenTheMostUpToDateEntryIsReturnedAsync()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();

        IRepository<SerializableEventCentricAggregateRoot> repository =
            Create<SerializableEventCentricAggregateRoot>();

        await repository.SaveAsync(aggregate);

        var context = new SerializableMessage();

        aggregate.Set(new SetRequest(context, Guid.NewGuid()));

        await repository.SaveAsync(aggregate);

        var other = new SerializableEventCentricAggregateRoot();

        await repository.SaveAsync(other);

        SerializableEventCentricAggregateRoot? actual = await repository.GetAsync(aggregate.Id);

        Assert.NotNull(actual);
        Assert.NotSame(aggregate, actual);
        Assert.Equal(aggregate.Id, actual!.Id);
        Assert.Equal(aggregate.Version, actual.Version);
    }

    [Fact]
    public async Task GivenAVersionThatIsNotTheCurrentVersionThenTheRequestedEntryIsReturned()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        Sequence expectedFirst = aggregate.Version;

        IRepository<SerializableEventCentricAggregateRoot> repository =
            Create<SerializableEventCentricAggregateRoot>();

        await repository.SaveAsync(aggregate);

        var context = new SerializableMessage();

        aggregate.Set(new SetRequest(context, Guid.NewGuid()));

        Sequence expectedSecond = aggregate.Version;

        await repository.SaveAsync(aggregate);

        var other = new SerializableEventCentricAggregateRoot();

        await repository.SaveAsync(other);

        SerializableEventCentricAggregateRoot? actualFirst = await repository.GetAsync(aggregate.Id, version: expectedFirst);
        SerializableEventCentricAggregateRoot? actualSecond = await repository.GetAsync(aggregate.Id, version: expectedSecond);

        Assert.NotNull(actualFirst);
        Assert.NotSame(expectedFirst, actualFirst);
        Assert.Equal(aggregate.Id, actualFirst!.Id);
        Assert.Equal(expectedFirst, actualFirst.Version);

        Assert.NotNull(actualSecond);
        Assert.NotSame(expectedSecond, actualSecond);
        Assert.Equal(aggregate.Id, actualSecond!.Id);
        Assert.Equal(expectedSecond, actualSecond.Version);
    }

    [Fact]
    public async Task GivenAVersionWhenNoExistingVersionedEntryMatchesThenANullValueIsReturnedAsync()
    {
        var aggregate = new SerializableAggregateRoot();
        var other = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(aggregate);
        await repository.SaveAsync(other);

        SerializableAggregateRoot? actual = await repository.GetAsync(aggregate.Id, version: other.Version);

        Assert.Null(actual);
    }

    protected override IRepository<TAggregate> Create<TAggregate>(ICloner cloner)
    {
        return new VersionedMemoryRepository<TAggregate>(cloner);
    }
}