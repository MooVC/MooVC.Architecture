namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using Xunit;

    public sealed class WhenIsNextIsCalled
    {
        private readonly SerializableAggregateRoot aggregate;

        public WhenIsNextIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
        }

        [Fact]
        public void GivenADifferentVersionThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            var other = new SerializableAggregateRoot();

            Assert.False(other.Version.IsNext(version));
        }

        [Fact]
        public void GivenADifferentNextVersionThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            var other = new SerializableAggregateRoot();
            SignedVersion next = other.Version.Next();

            Assert.False(next.IsNext(version));
        }

        [Fact]
        public void GivenAnEmptyVersionThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;

            Assert.False(version.IsNext(SignedVersion.Empty));
        }

        [Fact]
        public void GivenTheNextNextVersionThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next().Next();

            Assert.False(next.IsNext(version));
        }

        [Fact]
        public void GivenTheNextVersionThenTheResponseIsPositive()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.True(next.IsNext(version));
        }

        [Fact]
        public void GivenThePreviousVersionThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.False(version.IsNext(next));
        }

        [Fact]
        public void GivenADifferentVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            var other = new SerializableAggregateRoot();

            Assert.False(other.Version.IsNext(version.Footer, version.Number));
        }

        [Fact]
        public void GivenADifferentNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            var other = new SerializableAggregateRoot();
            SignedVersion next = other.Version.Next();

            Assert.False(next.IsNext(version.Footer, version.Number));
        }

        [Fact]
        public void GivenAnEmptyVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;

            Assert.False(version.IsNext(SignedVersion.Empty.Footer, SignedVersion.Empty.Number));
        }

        [Fact]
        public void GivenTheNextNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next().Next();

            Assert.False(next.IsNext(version.Footer, version.Number));
        }

        [Fact]
        public void GivenTheNextVersionWhenFooterAndNumberAreUsedThenTheResponseIsPositive()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.True(next.IsNext(version.Footer, version.Number));
        }

        [Fact]
        public void GivenThePreviousVersionWhenFooterAndNumberAreUsedThenTheResponseIsNegative()
        {
            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();

            Assert.False(version.IsNext(next.Footer, next.Number));
        }
    }
}