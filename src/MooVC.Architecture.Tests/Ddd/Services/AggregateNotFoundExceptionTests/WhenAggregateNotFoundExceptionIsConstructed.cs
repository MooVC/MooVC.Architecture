namespace MooVC.Architecture.Ddd.Services.AggregateNotFoundExceptionTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenAggregateNotFoundExceptionIsConstructed
    {
        [Fact]
        public void GivenAnAggregateAndAContextThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var aggregate = Guid.NewGuid().ToReference<AggregateRoot>();
            var context = new SerializableMessage();

            var instance = new AggregateNotFoundException<AggregateRoot>(
                context,
                aggregate);

            Assert.Equal(aggregate, instance.Aggregate);
            Assert.Equal(context, instance.Context);
        }

        [Fact]
        public void GivenAnEmptyAggregateAndAContextThenAnArgumentExceptionIsThrown()
        {
            Reference<AggregateRoot> aggregate = Reference<AggregateRoot>.Empty;
            var context = new SerializableMessage();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateNotFoundException<AggregateRoot>(context, aggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenANullAggregateAndAContextThenAnArgumentExceptionIsThrown()
        {
            Reference<AggregateRoot>? aggregate = default;
            var context = new SerializableMessage();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateNotFoundException<AggregateRoot>(context, aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateAndANullContextThenAnArgumentNullExceptionIsThrown()
        {
            var aggregate = Guid.NewGuid().ToReference<AggregateRoot>();
            Message? context = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateNotFoundException<AggregateRoot>(context!, aggregate));

            Assert.Equal(nameof(context), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateIdAndAContextThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var aggregateId = Guid.NewGuid();
            var context = new SerializableMessage();

            var instance = new AggregateNotFoundException<AggregateRoot>(
                context,
                aggregateId);

            Assert.Equal(aggregateId, instance.Aggregate.Id);
            Assert.Equal(context, instance.Context);
        }

        [Fact]
        public void GivenAnEmptyAggregateIdAndAContextThenAnArgumentExceptionIsThrown()
        {
            Guid aggregate = Guid.Empty;
            var context = new SerializableMessage();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateNotFoundException<AggregateRoot>(context, aggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateIdAndANullContextThenAnArgumentNullExceptionIsThrown()
        {
            var aggregateId = Guid.NewGuid();
            Message? context = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateNotFoundException<AggregateRoot>(context!, aggregateId));

            Assert.Equal(nameof(context), exception.ParamName);
        }
    }
}