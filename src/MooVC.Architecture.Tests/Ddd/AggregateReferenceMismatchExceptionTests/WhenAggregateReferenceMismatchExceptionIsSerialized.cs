namespace MooVC.Architecture.Ddd.AggregateReferenceMismatchExceptionTests;

using System;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateReferenceMismatchExceptionIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var reference = Guid.NewGuid().ToReference<SerializableAggregateRoot>();
        var original = new AggregateReferenceMismatchException<SerializableEventCentricAggregateRoot>(reference);
        AggregateReferenceMismatchException<SerializableEventCentricAggregateRoot> deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Reference, deserialized.Reference);
    }
}