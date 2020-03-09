namespace MooVC.Architecture.Ddd.VersionedReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenVersionedReferenceIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenTheIdTypeAndVersionArePropagated()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            Assert.Equal(aggregate.Id, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.Equal(aggregate.Version, reference.Version);
        }

        [Fact]
        public void GivenAnAggregateIdAndVersionThenTheIdTypeAndVersionArePropagated()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate.Id, aggregate.Version);

            Assert.Equal(aggregate.Id, reference.Id);
            Assert.Equal(typeof(AggregateRoot), reference.Type);
            Assert.Equal(aggregate.Version, reference.Version);
        }

        [Fact]
        public void GivenAnEmptyIdThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableAggregateRoot();

            _ = Assert.Throws<ArgumentException>(() => new VersionedReference<AggregateRoot>(Guid.Empty, aggregate.Version));
        }

        [Fact]
        public void GivenAnIdAndANullVersionThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(() => new VersionedReference<AggregateRoot>(Guid.NewGuid(), null));
        }
    }
}