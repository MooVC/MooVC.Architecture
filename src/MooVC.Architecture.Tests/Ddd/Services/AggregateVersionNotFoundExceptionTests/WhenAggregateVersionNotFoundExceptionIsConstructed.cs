namespace MooVC.Architecture.Ddd.Services.AggregateVersionNotFoundExceptionTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenAggregateVersionNotFoundExceptionIsConstructed
{
    [Fact]
    public void GivenAnAggregateAndAContextThenAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        var context = new SerializableMessage();
        var instance = new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregate, context);

        Assert.Equal(aggregate, instance.Aggregate);
        Assert.Equal(context, instance.Context);
    }

    [Fact]
    public void GivenAnEmptyAggregateAndAContextThenAnArgumentExceptionIsThrown()
    {
        Reference<AggregateRoot> aggregate = Reference<AggregateRoot>.Empty;
        var context = new SerializableMessage();

        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            new AggregateVersionNotFoundException<AggregateRoot>(aggregate, context));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenAnNullAggregateAndAContextThenAnArgumentExceptionIsThrown()
    {
        Reference<AggregateRoot>? aggregate = default;
        var context = new SerializableMessage();

        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            new AggregateVersionNotFoundException<AggregateRoot>(aggregate!, context));

        Assert.Equal(nameof(aggregate), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateAndANullContextThenAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        Message? context = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
            new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregate, context!));

        Assert.Equal(nameof(context), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdAContextAndAVersionThenAnInstanceIsReturnedWithAllPropertiesSet()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        var context = new SerializableMessage();
        SignedVersion version = subject.Version;

        var instance = new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregateId, context, version: version);

        Assert.Equal(aggregateId, instance.Aggregate.Id);
        Assert.Equal(context, instance.Context);
        Assert.Equal(version, instance.Aggregate.Version);
    }

    [Fact]
    public void GivenAnEmptyAggregateIdAContextAndAVersionThenAnArgumentExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid id = Guid.Empty;
        var context = new SerializableMessage();
        SignedVersion version = subject.Version;

        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            new AggregateVersionNotFoundException<AggregateRoot>(id, context, version: version));

        Assert.Equal(nameof(id), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdANullContextAndAVersionThenAnArgumentNullExceptionIsThrown()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        Message? context = default;
        SignedVersion version = subject.Version;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() =>
            new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregateId, context!, version: version));

        Assert.Equal(nameof(context), exception.ParamName);
    }

    [Fact]
    public void GivenAnAggregateIdAContextAndANullVersionThenAnInstanceIsReturnedWithAnEmptyVersion()
    {
        var subject = new SerializableAggregateRoot();
        Guid aggregateId = subject.Id;
        var context = new SerializableMessage();
        SignedVersion? version = default;
        var instance = new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregateId, context, version: version);

        Assert.False(instance.Aggregate.IsVersioned);
    }
}