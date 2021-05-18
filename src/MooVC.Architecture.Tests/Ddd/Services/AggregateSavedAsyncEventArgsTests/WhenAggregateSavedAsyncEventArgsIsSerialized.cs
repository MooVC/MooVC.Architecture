namespace MooVC.Architecture.Ddd.Services.AggregateSavedAsyncEventArgsTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateSavedAsyncEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var @event = new AggregateSavedAsyncEventArgs<SerializableAggregateRoot>(aggregate);

            AggregateSavedAsyncEventArgs<SerializableAggregateRoot> deserialized = @event.Clone();

            Assert.Equal(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event, deserialized);
        }
    }
}