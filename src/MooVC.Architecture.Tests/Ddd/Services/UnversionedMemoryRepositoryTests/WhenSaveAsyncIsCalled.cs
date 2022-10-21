namespace MooVC.Architecture.Ddd.Services.UnversionedMemoryRepositoryTests;

using System.Threading.Tasks;
using MooVC.Architecture.Serialization;
using Xunit;

public class WhenSaveAsyncIsCalled
    : UnversionedMemoryRepositoryTests
{
    [Fact]
    public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheAggregateIsAddedAndTheVersionIncrementedAsync()
    {
        var expected = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(expected);

        SerializableAggregateRoot? actual = await repository.GetAsync(expected.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected, actual);
        Assert.NotSame(expected, actual);
    }

    [Fact]
    public async Task GivenANewAggregateWhenAnExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrownAsync()
    {
        var saved = new SerializableAggregateRoot();
        var pending = new SerializableAggregateRoot(saved.Id);

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(saved);

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.SaveAsync(pending));

        Assert.Equal(saved.Id, exception.Aggregate.Id);
        Assert.Equal(saved.Version, exception.Persisted);
    }

    [Fact]
    public async Task GivenANonNewAnAggregateWhenNoExistingMemberWithTheSameIdExistsThenAnAggregateConflictDetectedExceptionIsThrownAsync()
    {
        var aggregate = new SerializableAggregateRoot();

        aggregate.MarkChangesAsCommitted();
        aggregate.Set();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.SaveAsync(aggregate));

        Assert.Equal(aggregate.Id, exception.Aggregate.Id);
        Assert.Equal(SignedVersion.Empty, exception.Persisted);
    }

    [Fact]
    public async Task GivenAnAggregateWhenAnExistingMemberHasAHigherVersionThenAnAggregateConflictDetectedExceptionIsThrownAsync()
    {
        var saved = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(saved);

        SerializableAggregateRoot pending = saved.Clone();

        saved.Set();

        await repository.SaveAsync(saved);

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.SaveAsync(pending));

        Assert.Equal(saved.Id, exception.Aggregate.Id);
        Assert.Equal(saved.Version, exception.Persisted);
    }

    [Fact]
    public async Task GivenAnAggregateWhenAnExistingMemberHasALowerVersionThenAnAggregateConflictDetectedExceptionIsThrownAsync()
    {
        var saved = new SerializableAggregateRoot();

        IRepository<SerializableAggregateRoot> repository =
            Create<SerializableAggregateRoot>();

        await repository.SaveAsync(saved);

        SerializableAggregateRoot pending = saved.Clone();

        pending.Set();
        pending.MarkChangesAsCommitted();
        pending.Set();
        pending.MarkChangesAsCommitted();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            await Assert.ThrowsAsync<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => repository.SaveAsync(pending));

        Assert.Equal(saved.Id, exception.Aggregate.Id);
        Assert.Equal(saved.Version, exception.Persisted);
    }

    [Fact]
    public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavedEventIsRaisedPriorToTheVersionIncrementAsync()
    {
        var expectedAggregate = new SerializableAggregateRoot();
        bool wasInvoked = false;

        IRepository<SerializableAggregateRoot> expectedRepository =
            Create<SerializableAggregateRoot>();

        Task Aggregate_Saved(
            IRepository<SerializableAggregateRoot> actualRepository,
            AggregateSavedAsyncEventArgs<SerializableAggregateRoot> e)
        {
            Assert.Equal(expectedRepository, actualRepository);
            Assert.Equal(expectedAggregate, e.Aggregate);
            Assert.Same(expectedAggregate, e.Aggregate);
            Assert.True(e.Aggregate.Version.IsNew);

            return Task.FromResult(wasInvoked = true);
        }

        expectedRepository.Saved += Aggregate_Saved;

        await expectedRepository.SaveAsync(expectedAggregate);

        Assert.True(wasInvoked);
    }

    [Fact]
    public async Task GivenANewAggregateWhenNoExistingMemberWithTheSameIdExistsThenTheSavingEventIsRaisedPriorToTheVersionIncrementAsync()
    {
        var expectedAggregate = new SerializableAggregateRoot();
        bool wasInvoked = false;

        IRepository<SerializableAggregateRoot> expectedRepository =
            Create<SerializableAggregateRoot>();

        Task Aggregate_Saving(
            IRepository<SerializableAggregateRoot> actualRepository,
            AggregateSavingAsyncEventArgs<SerializableAggregateRoot> e)
        {
            Assert.Equal(expectedRepository, actualRepository);
            Assert.Equal(expectedAggregate, e.Aggregate);
            Assert.Same(expectedAggregate, e.Aggregate);
            Assert.True(e.Aggregate.Version.IsNew);

            return Task.FromResult(wasInvoked = true);
        }

        expectedRepository.Saving += Aggregate_Saving;

        await expectedRepository.SaveAsync(expectedAggregate);

        Assert.True(wasInvoked);
    }
}