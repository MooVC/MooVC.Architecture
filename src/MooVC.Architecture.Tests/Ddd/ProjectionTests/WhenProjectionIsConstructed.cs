namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using System;
    using Xunit;

    public sealed class WhenProjectionIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsReturnedWithTheAggregateReferenceSet()
        {
            var aggregate = new SerializableAggregateRoot();
            var instance = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var expected = aggregate.ToReference();

            Assert.Equal(expected, instance.Aggregate);
        }

        [Fact]
        public void GivenAnAggregateReferenceThenAnInstanceIsReturnedWithTheAggregateReferenceSet()
        {
            var aggregate = new SerializableAggregateRoot();
            var expected = aggregate.ToReference();
            var instance = new SerializableProjection<SerializableAggregateRoot>(expected);

            Assert.Equal(expected, instance.Aggregate);
        }

        [Fact]
        public void GivenAnEmptyAggregateReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference<SerializableAggregateRoot> aggregate = Reference<SerializableAggregateRoot>.Empty;

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
            Reference<SerializableAggregateRoot>? aggregate = default;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new SerializableProjection<SerializableAggregateRoot>(aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }
    }
}