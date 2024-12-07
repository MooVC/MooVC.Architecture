namespace MooVC.Architecture.Ddd.SignedVersionTests;

using System.Linq;
using Xunit;

public sealed class WhenSignedVersionIsOrdered
{
    [Fact]
    public void GivenAnUnorderedSequenceThenTheSequencedIsOrderedAscending()
    {
        Prepare(out Sequence first, out Sequence second, out Sequence third, out Sequence[] sequence);

        Sequence[] expected = new[] { first, second, third };
        Sequence[] actual = sequence.OrderBy(version => version).ToArray();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GivenAnUnorderedSequenceThenTheSequencedIsOrderedDescending()
    {
        Prepare(out Sequence first, out Sequence second, out Sequence third, out Sequence[] sequence);

        Sequence[] expected = new[] { third, second, first };
        Sequence[] actual = sequence.OrderByDescending(version => version).ToArray();

        Assert.Equal(expected, actual);
    }

    private static void Prepare(
        out Sequence first,
        out Sequence second,
        out Sequence third,
        out Sequence[] sequence)
    {
        var aggregate = new SerializableAggregateRoot();

        first = aggregate.Version;
        second = first.Next();
        third = second.Next();
        sequence = new[] { first, third, second };
    }
}