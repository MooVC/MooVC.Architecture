namespace MooVC.Architecture.Ddd.Services.EnsureTests;

using MooVC.Architecture.Serialization;
using Xunit;
using static MooVC.Architecture.Ddd.Services.Ensure;

public sealed class WhenAggregateDoesNotConflictIsCalled
{
    [Fact]
    public void GivenANewAggregateAndAnExistingAggregateThatConflictsThenAnAggregateConflictDetectedExceptionIsThrown()
    {
        var current = new SerializableAggregateRoot();
        var proposed = new SerializableAggregateRoot();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => AggregateDoesNotConflict(proposed, current));

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

        AggregateDoesNotConflict(proposed, current);
    }

    [Fact]
    public void GivenANewAggregateAndAnExistingVersionThatConflictsThenAnAggregateConflictDetectedExceptionIsThrown()
    {
        var proposed = new SerializableAggregateRoot();
        SignedVersion current = proposed.Version.Next();

        AggregateConflictDetectedException<SerializableAggregateRoot> exception =
            Assert.Throws<AggregateConflictDetectedException<SerializableAggregateRoot>>(
                () => AggregateDoesNotConflict(proposed, currentVersion: current));

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

        AggregateDoesNotConflict(proposed, currentVersion: current.Version);
    }

    [Fact]
    public void GivenANewAggregateAndNoCurrentAggregateThenNoExceptionIsThrown()
    {
        var proposed = new SerializableAggregateRoot();
        SerializableAggregateRoot? current = default;

        AggregateDoesNotConflict(proposed, current);
    }

    [Fact]
    public void GivenANewAggregateAndNoCurrentVersionThenNoExceptionIsThrown()
    {
        var aggregate = new SerializableAggregateRoot();

        AggregateDoesNotConflict(aggregate);
    }
}