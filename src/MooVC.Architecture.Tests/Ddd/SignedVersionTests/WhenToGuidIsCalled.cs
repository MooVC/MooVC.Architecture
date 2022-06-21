namespace MooVC.Architecture.Ddd.SignedVersionTests;

using System;
using System.Linq;
using Xunit;

public sealed class WhenToGuidIsCalled
{
    [Fact]
    public void GivenAnEmptyVersionThenAEmptyGuidIsReturned()
    {
        var id = SignedVersion.Empty.ToGuid();

        Assert.Equal(Guid.Empty, id);
    }

    [Fact]
    public void GivenAnVersionThenAGuidMatchingThatVersionIsReturned()
    {
        var aggregate = new SerializableAggregateRoot();
        SignedVersion version = aggregate.Version.Next();

        var expected = new Guid(new[]
        {
            version.Header.ElementAt(0),
            version.Header.ElementAt(1),
            version.Header.ElementAt(2),
            version.Header.ElementAt(3),
            version.Header.ElementAt(4),
            version.Header.ElementAt(5),
            version.Header.ElementAt(6),
            version.Header.ElementAt(7),
            version.Footer.ElementAt(0),
            version.Footer.ElementAt(1),
            version.Footer.ElementAt(2),
            version.Footer.ElementAt(3),
            version.Footer.ElementAt(4),
            version.Footer.ElementAt(5),
            version.Footer.ElementAt(6),
            version.Footer.ElementAt(7),
        });

        var actual = version.ToGuid();

        Assert.Equal(expected, actual);
    }
}