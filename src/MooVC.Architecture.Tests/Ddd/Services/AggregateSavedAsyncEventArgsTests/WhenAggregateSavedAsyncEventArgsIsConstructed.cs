namespace MooVC.Architecture.Ddd.Services.AggregateSavedAsyncEventArgsTests
{
    using System;
    using Xunit;

    public sealed class WhenAggregateSavedAsyncEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnAggregateThenAnInstanceIsCreated()
        {
            var aggregate = new SerializableAggregateRoot();
            var @event = new AggregateSavedAsyncEventArgs<SerializableAggregateRoot>(aggregate);

            Assert.Equal(aggregate, @event.Aggregate);
            Assert.Same(aggregate, @event.Aggregate);
        }

        [Fact]
        public void GivenANullAggregateThenAnArgumentNullExceptionIsThrown()
        {
            SerializableAggregateRoot? aggregate = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateSavedAsyncEventArgs<SerializableAggregateRoot>(aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }
    }
}