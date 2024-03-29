namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests;

using System;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
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
    public async Task GivenAVersionThatIsNotTheCurrentVersionThenANullValueIsReturnedAsync()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        SignedVersion expectedFirst = aggregate.Version;

        IRepository<SerializableEventCentricAggregateRoot> repository =
            Create<SerializableEventCentricAggregateRoot>();

        await repository.SaveAsync(aggregate);

        var context = new SerializableMessage();

        aggregate.Set(new SetRequest(context, Guid.NewGuid()));

        SignedVersion expectedSecond = aggregate.Version;

        await repository.SaveAsync(aggregate);

        var other = new SerializableEventCentricAggregateRoot();

        await repository.SaveAsync(other);

        SerializableEventCentricAggregateRoot? actualFirst = await repository.GetAsync(aggregate.Id, version: expectedFirst);
        SerializableEventCentricAggregateRoot? actualSecond = await repository.GetAsync(aggregate.Id, version: expectedSecond);

        Assert.Null(actualFirst);

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
}