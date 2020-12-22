namespace MooVC.Architecture.Ddd.Services.AggregateSavingEventArgsTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateSavingEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var @event = new AggregateSavingEventArgs<SerializableAggregateRoot>(aggregate);

            AggregateSavingEventArgs<SerializableAggregateRoot> deserialized = @event.Clone();

            Assert.Equal(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event, deserialized);
        }
    }
}