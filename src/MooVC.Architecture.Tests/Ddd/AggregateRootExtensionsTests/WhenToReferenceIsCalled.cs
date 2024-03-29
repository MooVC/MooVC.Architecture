﻿namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests;

using System;
using Xunit;

public sealed class WhenToReferenceIsCalled
{
    [Fact]
    public void GivenAnAbstractAggregateThenAReferenceWithTheSameIdTypeAndVersionIsReturned()
    {
        var aggregateId = Guid.NewGuid();
        AggregateRoot aggregate = new SerializableAggregateRoot(aggregateId);

        var reference = aggregate.ToReference();

        Assert.Equal(aggregateId, reference.Id);
        Assert.Equal(aggregate.Version, reference.Version);
        Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
    }

    [Fact]
    public void GivenAnAggregateThenAReferenceWithTheSameIdTypeAndVersionIsReturned()
    {
        var aggregateId = Guid.NewGuid();
        var aggregate = new SerializableAggregateRoot(aggregateId);

        var reference = aggregate.ToReference();

        Assert.Equal(aggregateId, reference.Id);
        Assert.Equal(aggregate.Version, reference.Version);
        Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
    }

    [Fact]
    public void GivenAnAbstractAggregateWhenUnversionedIsTrueThenAReferenceWithTheSameIdAndTypeIsReturned()
    {
        var aggregateId = Guid.NewGuid();
        AggregateRoot aggregate = new SerializableAggregateRoot(aggregateId);

        var reference = aggregate.ToReference(unversioned: true);

        Assert.Equal(aggregateId, reference.Id);
        Assert.False(reference.IsVersioned);
        Assert.NotEqual(aggregate.Version, reference.Version);
        Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
    }

    [Fact]
    public void GivenAnAggregateWhenUnversionedIsTrueThenAReferenceWithTheSameIdAndTypeIsReturned()
    {
        var aggregateId = Guid.NewGuid();
        var aggregate = new SerializableAggregateRoot(aggregateId);

        var reference = aggregate.ToReference(unversioned: true);

        Assert.Equal(aggregateId, reference.Id);
        Assert.False(reference.IsVersioned);
        Assert.NotEqual(aggregate.Version, reference.Version);
        Assert.Equal(typeof(SerializableAggregateRoot), reference.Type);
    }
}