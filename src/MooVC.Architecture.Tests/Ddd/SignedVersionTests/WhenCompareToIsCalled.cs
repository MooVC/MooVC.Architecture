namespace MooVC.Architecture.Ddd.SignedVersionTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenCompareToIsCalled
    {
        private readonly SerializableAggregateRoot aggregate;

        public WhenCompareToIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
        }

        [Fact]
        public void GivenAnEarlierVersionThenPositiveOneIsReturned()
        {
            const int ExpectedValue = 1;

            SignedVersion version = aggregate.Version;
            int actualValue = version.CompareTo(SignedVersion.Empty);

            Assert.Equal(ExpectedValue, actualValue);
        }

        [Fact]
        public void GivenALaterVersionThenNegativeOneIsReturned()
        {
            const int ExpectedValue = -1;

            SignedVersion version = aggregate.Version;
            int actualValue = SignedVersion.Empty.CompareTo(version);

            Assert.Equal(ExpectedValue, actualValue);
        }

        [Fact]
        public void GivenANullVersionThenPositiveOneIsReturned()
        {
            const int ExpectedValue = 1;

            SignedVersion version = aggregate.Version;
            int actualValue = version.CompareTo(default);

            Assert.Equal(ExpectedValue, actualValue);
        }

        [Fact]
        public void GivenAFutureVersionThenNegativeOneIsReturned()
        {
            const int ExpectedValue = -1;

            SignedVersion version = aggregate.Version;
            SignedVersion future = version.Next().Next();
            int actualValue = version.CompareTo(future);

            Assert.Equal(ExpectedValue, actualValue);
        }

        [Fact]
        public void GivenTheNextVersionThenNegativeOneIsReturned()
        {
            const int ExpectedValue = -1;

            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();
            int actualValue = version.CompareTo(next);

            Assert.Equal(ExpectedValue, actualValue);
        }

        [Fact]
        public void GivenThePreviousVersionThenPositiveOneIsReturned()
        {
            const int ExpectedValue = 1;

            SignedVersion version = aggregate.Version;
            SignedVersion next = version.Next();
            int actualValue = next.CompareTo(version);

            Assert.Equal(ExpectedValue, actualValue);
        }
    }
}