namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MooVC.Architecture.Ddd.AggregateRootTests;
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
    }
}
