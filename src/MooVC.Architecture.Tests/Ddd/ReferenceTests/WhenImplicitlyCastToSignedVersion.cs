namespace MooVC.Architecture.Ddd.ReferenceTests;

using Xunit;

public sealed class WhenImplicitlyCastToSignedVersion
{
    [Fact]
    public void GivenAnEmptyReferenceThenAnEmptyVersionIsReturned()
    {
        Reference reference = Reference<SerializableAggregateRoot>.Empty;
        Sequence version = reference;

        Assert.Equal(reference.Version, version);
        Assert.True(version.IsEmpty);
    }

    [Fact]
    public void GivenAnReferenceThenTheVersionOfThatReferenceIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();
        Sequence version = reference;

        Assert.Equal(reference.Version, version);
        Assert.Equal(aggregate.Version, version);
        Assert.False(version.IsEmpty);
        Assert.True(version.IsNew);
    }

    [Fact]
    public void GivenANullReferenceThenAnEmptyVersionIsReturned()
    {
        Reference? reference = default;
        Sequence version = reference;

        Assert.Equal(Sequence.Empty, version);
        Assert.True(version.IsEmpty);
    }
}