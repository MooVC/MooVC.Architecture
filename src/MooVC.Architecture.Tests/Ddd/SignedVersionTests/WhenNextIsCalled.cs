namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenNextIsCalled
    {
        private readonly SerializableAggregateRoot aggregate;

        public WhenNextIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
        }

        [Fact]
        public void GivenAVersionThenTheNextVersionIsReturned()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.True(next.IsNext(version));
        }

        [Fact]
        public void GivenAVersionThenTheHeaderOfTheNextVersionIsTheFooterOfThePreviousVersion()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.Equal(version.Footer, next.Header);
        }

        [Fact]
        public void GivenAVersionThenTheHeaderOfTheNextVersionNumberIsOneHigherThanThePreviousVersion()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.True(next.Number - version.Number == 1);
        }
    }
}
