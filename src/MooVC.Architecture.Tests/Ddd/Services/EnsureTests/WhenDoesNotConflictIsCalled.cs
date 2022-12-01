namespace MooVC.Architecture.Ddd.Services.EnsureTests;

using MooVC.Architecture.Serialization;
using Xunit;
using static MooVC.Architecture.Ddd.Services.Ensure;

public sealed class WhenDoesNotConflictIsCalled
{
    [Fact]
    public void GivenANewAggregateAndAnExistingAggregateThatConflictsThenAnAggregateConflictDetectedExceptionIsThrown()
    {
        var current = new SerializableAggregateRoot();
        var proposed = new SerializableAggregateRoot();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => DoesNotConflict(proposed, current));

        Assert.Equal(proposed.Version, exception.Received);
        Assert.Equal(current.Version, exception.Persisted);
    }

    [Fact]
    public void GivenANewAggregateAndAnExistingAggregateThatDoesNotConflictThenNoExceptionIsThrown()
    {
        var current = new SerializableAggregateRoot();

        current.MarkChangesAsCommitted();

        SerializableAggregateRoot proposed = current.Clone();

        proposed.Set();
        proposed.MarkChangesAsCommitted();

        DoesNotConflict(proposed, current);
    }

    [Fact]
    public void GivenANewAggregateAndAnExistingVersionThatConflictsThenAnAggregateConflictDetectedExceptionIsThrown()
    {
        var proposed = new SerializableAggregateRoot();
        SignedVersion current = proposed.Version.Next();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => DoesNotConflict(proposed, currentVersion: current));

        Assert.Equal(proposed.Version, exception.Received);
        Assert.Equal(current, exception.Persisted);
    }

    [Fact]
    public void GivenANewAggregateAndAnExistingVersionThatDoesNotConflictThenNoExceptionIsThrown()
    {
        var current = new SerializableAggregateRoot();

        current.MarkChangesAsCommitted();

        SerializableAggregateRoot proposed = current.Clone();

        proposed.Set();
        proposed.MarkChangesAsCommitted();

        DoesNotConflict(proposed, currentVersion: current.Version);
    }

    [Fact]
    public void GivenANewAggregateAndNoCurrentAggregateThenNoExceptionIsThrown()
    {
        var proposed = new SerializableAggregateRoot();
        SerializableAggregateRoot? current = default;

        DoesNotConflict(proposed, current);
    }

    [Fact]
    public void GivenANewAggregateAndNoCurrentVersionThenNoExceptionIsThrown()
    {
        var aggregate = new SerializableAggregateRoot();

        DoesNotConflict(aggregate);
    }
}