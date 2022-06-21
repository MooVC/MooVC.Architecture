namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public class WhenGetAllAsyncIsCalled
    : UnversionedMemoryRepositoryTests
{
    [Fact]
    public async Task GivenAnEmptyRepositoryThenAnEmptyEnumerableIsReturnedAsync()
    {
        IRepository<SerializableAggregateRoot> repository = Create<SerializableAggregateRoot>();
        IEnumerable<SerializableAggregateRoot> results = await repository.GetAllAsync();

        Assert.Empty(results);
    }

    [Fact]
    public async Task GivenAPopulatedRepositoryThenAListOfTheMostUpToDateVersionsIsReturnedAsync()
    {
        const int ExpectedTotal = 2;

        var first = new SerializableEventCentricAggregateRoot();
        var second = new SerializableEventCentricAggregateRoot();

        IRepository<SerializableEventCentricAggregateRoot> repository =
            Create<SerializableEventCentricAggregateRoot>();

        await repository.SaveAsync(first);
        await repository.SaveAsync(second);

        var context = new SerializableMessage();

        second.Set(new SetRequest(context, Guid.NewGuid()));

        await repository.SaveAsync(second);

        IEnumerable<SerializableEventCentricAggregateRoot> results = await repository.GetAllAsync();

        Assert.Equal(ExpectedTotal, results.Count());
        Assert.Contains(results, result => result.Id == first.Id && result.Version == first.Version);
        Assert.Contains(results, result => result.Id == second.Id && result.Version == second.Version);
    }
}