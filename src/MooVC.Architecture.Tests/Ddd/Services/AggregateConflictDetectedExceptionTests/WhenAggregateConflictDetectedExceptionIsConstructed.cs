namespace MooVC.Architecture.Ddd.Services.AggregateConflictDetectedExceptionTests;

using System;
using Xunit;

public sealed class WhenAggregateConflictDetectedExceptionIsConstructed
{
    [Fact]
    public void GivenAnAggregateAndAReceivedVersionTheAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Sequence received = subject.Version;

        var instance = new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, received);

        Assert.Equal(aggregate, instance.Aggregate);
        Assert.Equal(received, instance.Received);
        Assert.Equal(Sequence.Empty, instance.Persisted);
    }

    [Fact]
    public void GivenAnAggregateAndANullReceivedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Sequence? received = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, received!));

        Assert.Equal(nameof(received), exception.ParamName);
    }

    [Fact]
    public void GivenAEmptyAggregateAndAReceivedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Reference<SerializableAggregateRoot> aggregate = Reference<SerializableAggregateRoot>.Empty;
        Sequence received = subject.Version;

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, received));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenANullAggregateAndAReceivedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Reference<SerializableAggregateRoot>? aggregate = default;
        Sequence received = subject.Version;

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate!, received));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdAndAReceivedVersionTheAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Sequence received = subject.Version;

        var instance = new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregateId, received);

        Assert.Equal(aggregateId, instance.Aggregate.Id);
        Assert.Equal(received, instance.Received);
        Assert.Equal(Sequence.Empty, instance.Persisted);
    }

    [Fact]
    public void GivenAnAggregateIdAndANullReceivedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Sequence? received = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregateId, received!));

        Assert.Equal(nameof(received), exception.ParamName);
    }

    [Fact]
    public void GivenAEmptyAggregateIdAndAReceivedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid id = Guid.Empty;
        Sequence received = subject.Version;

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(id, received));

        Assert.Equal(nameof(id), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateAReceivedVersionAndAPersistedVersionTheAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Sequence received = subject.Version;
        Sequence persisted = subject.Version.Next();

        var instance = new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, persisted, received);

        Assert.Equal(aggregate, instance.Aggregate);
        Assert.Equal(received, instance.Received);
        Assert.Equal(persisted, instance.Persisted);
    }

    [Fact]
    public void GivenAnAggregateAndANullReceivedVersionAndAPersistedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Sequence? received = default;
        Sequence persisted = subject.Version.Next();

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, persisted, received!));

        Assert.Equal(nameof(received), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateAReceivedVersionAndANullPersistedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Sequence received = subject.Version;
        Sequence? persisted = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, persisted!, received));

        Assert.Equal(nameof(persisted), exception.ParamName);
    }

    [Fact]
    public void GivenAEmptyAggregateAReceivedVersionAndAPersistedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Reference<SerializableAggregateRoot> aggregate = Reference<SerializableAggregateRoot>.Empty;
        Sequence received = subject.Version;
        Sequence persisted = subject.Version.Next();

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate, persisted, received));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenANullAggregateAReceivedVersionAndAPersistedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Reference<SerializableAggregateRoot>? aggregate = default;
        Sequence received = subject.Version;
        Sequence persisted = subject.Version.Next();

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregate!, persisted, received));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdAReceivedVersionAndAPersistedTheAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Sequence received = subject.Version;
        Sequence persisted = subject.Version.Next();

        var instance = new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregateId, persisted, received);

        Assert.Equal(aggregateId, instance.Aggregate.Id);
        Assert.Equal(received, instance.Received);
        Assert.Equal(persisted, instance.Persisted);
    }

    [Fact]
    public void GivenAnAggregateIdANullReceivedVersionAndAPersistedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Sequence? received = default;
        Sequence persisted = subject.Version.Next();

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregateId, persisted, received!));

        Assert.Equal(nameof(received), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdAReceivedVersionAndANullPersistedVersionTheAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Sequence received = subject.Version;
        Sequence? persisted = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(aggregateId, persisted!, received));

        Assert.Equal(nameof(persisted), exception.ParamName);
    }

    [Fact]
    public void GivenAEmptyAggregateIdAReceivedVersionAndAPersistedVersionTheAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid id = Guid.Empty;
        Sequence received = subject.Version;
        Sequence persisted = subject.Version.Next();

        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => new AggregateConflictDetectedException<SerializableAggregateRoot>(id, persisted, received));

        Assert.Equal(nameof(id), exception.ParamName);
    }
}