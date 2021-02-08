namespace MooVC.Architecture.Ddd.AggregateEventMismatchExceptionTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenAggregateEventMismatchExceptionIsConstructed
    {
        [Fact]
        public void GivenNoAggregateAndTheEventAggregateThenAnArgumentNullExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            VersionedReference? aggregate = default;
            var eventAggregate = subject.ToVersionedReference();

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateEventMismatchException(aggregate!, eventAggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenTheAggregateAndTheEventAggregateThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var subject = new SerializableAggregateRoot();
            var aggregate = subject.ToVersionedReference();
            var eventAggregate = subject.ToVersionedReference();
            var instance = new AggregateEventMismatchException(aggregate, eventAggregate);

            Assert.Equal(aggregate, instance.Aggregate);
            Assert.Equal(eventAggregate, instance.EventAggregate);
        }

        [Fact]
        public void GivenTheAggregateAndNoEventAggregateThenAnArgumentNullExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            var aggregate = subject.ToVersionedReference();
            VersionedReference? eventAggregate = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateEventMismatchException(aggregate, eventAggregate!));

            Assert.Equal(nameof(eventAggregate), exception.ParamName);
        }
    }
}