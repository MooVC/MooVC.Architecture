namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using Xunit;

    public sealed class WhenImplicityCastToSignedVersion
    {
        [Fact]
        public void GivenAnAggregateThenTheVersionOfThatAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            SignedVersion version = aggregate;

            Assert.Equal(aggregate.Version, version);
        }

        [Fact]
        public void GivenNoAggregateThenAnEmptyVersionIsReturned()
        {
            SerializableAggregateRoot? aggregate = default;
            SignedVersion version = aggregate;

            Assert.Equal(SignedVersion.Empty, version);
        }
    }
}