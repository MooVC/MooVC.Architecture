﻿namespace MooVC.Architecture.Ddd.SignedVersionTests;

using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenSignedVersionIsSerialized
{
    [Fact]
    public void GivenASignedVersionThenAllPropertiesArePropagated()
    {
        var aggregate = new SerializableAggregateRoot();
        SignedVersion original = aggregate.Version;
        SignedVersion deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original, deserialized);
    }
}