namespace MooVC.Architecture.Ddd.Services.AggregateSavedEventArgsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Xunit;

    public sealed class WhenAggregateSavedEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsCreated()
        {
            var aggregate = new SerializableAggregateRoot();
            var @event = new AggregateSavedEventArgs<SerializableAggregateRoot>(aggregate);

            Assert.Equal(aggregate, @event.Aggregate);
            Assert.Same(aggregate, @event.Aggregate);
        }

        [Fact]
        public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
        {
            SerializableAggregateRoot aggregate = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateSavedEventArgs<SerializableAggregateRoot>(aggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }
    }
}