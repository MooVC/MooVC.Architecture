namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenProjectionIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsReturnedWithTheAggregateReferenceSet()
        {
            var aggregate = new SerializableAggregateRoot();
            var instance = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var expected = aggregate.ToVersionedReference();

            Assert.Equal(expected, instance.Aggregate);
        }

        [Fact]
        public void GivenAnAggregateReferenceThenAnInstanceIsReturnedWithTheAggregateReferenceSet()
        {
            var aggregate = new SerializableAggregateRoot();
            var expected = aggregate.ToVersionedReference();
            var instance = new SerializableProjection<SerializableAggregateRoot>(expected);

            Assert.Equal(expected, instance.Aggregate);
        }

        [Fact]
        public void GivenAnEmptyAggregateReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference<SerializableAggregateRoot> aggregate = VersionedReference<SerializableAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new SerializableProjection<SerializableAggregateRoot>(aggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnNullAggregateThenAnArgumentExceptionIsThrown()
        {
            SerializableAggregateRoot? aggregate = default;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new SerializableProjection<SerializableAggregateRoot>(aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnNullAggregateReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference<SerializableAggregateRoot>? aggregate = default;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new SerializableProjection<SerializableAggregateRoot>(aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }
    }
}