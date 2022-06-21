namespace MooVC.Architecture.Ddd.SignedVersionTests;

using System;
using Xunit;

public sealed class WhenImplicitlyCastToGuid
{
    [Fact]
    public void GivenAnEmptyVersionThenAnEmptyGuidIsReturned()
    {
        SignedVersion version = SignedVersion.Empty;
        Guid signature = version;

        Assert.Equal(version.Signature, signature);
        Assert.Equal(Guid.Empty, signature);
    }

    [Fact]
    public void GivenAnVersionThenTheVersionSignatureIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        SignedVersion version = aggregate.Version;
        Guid signature = version;

        Assert.Equal(version.Signature, signature);
    }

    [Fact]
    public void GivenANullVersionThenAnEmptyGuidIsReturned()
    {
        SignedVersion? version = default;
        Guid signature = version;

        Assert.Equal(Guid.Empty, signature);
    }
}