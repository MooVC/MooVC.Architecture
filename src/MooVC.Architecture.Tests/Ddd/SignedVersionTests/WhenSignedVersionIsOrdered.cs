namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using System.Linq;
    using Xunit;

    public sealed class WhenSignedVersionIsOrdered
    {
        [Fact]
        public void GivenAnUnorderedSequenceThenTheSequencedIsOrderedAscending()
        {
            Prepare(out SignedVersion first, out SignedVersion second, out SignedVersion third, out SignedVersion[] sequence);

            SignedVersion[] expected = new[] { first, second, third };
            SignedVersion[] actual = sequence.OrderBy(version => version).ToArray();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenAnUnorderedSequenceThenTheSequencedIsOrderedDescending()
        {
            Prepare(out SignedVersion first, out SignedVersion second, out SignedVersion third, out SignedVersion[] sequence);

            SignedVersion[] expected = new[] { third, second, first };
            SignedVersion[] actual = sequence.OrderByDescending(version => version).ToArray();

            Assert.Equal(expected, actual);
        }

        private static void Prepare(
            out SignedVersion first,
            out SignedVersion second,
            out SignedVersion third,
            out SignedVersion[] sequence)
        {
            var aggregate = new SerializableAggregateRoot();

            first = aggregate.Version;
            second = first.Next();
            third = second.Next();
            sequence = new[] { first, third, second };
        }
    }
}