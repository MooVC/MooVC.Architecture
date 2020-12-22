namespace MooVC.Architecture.Ddd.Services.AggregateSavedEventArgsTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAggregateSavedEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var @event = new AggregateSavedEventArgs<SerializableAggregateRoot>(aggregate);

            AggregateSavedEventArgs<SerializableAggregateRoot> deserialized = @event.Clone();

            Assert.Equal(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event.Aggregate, deserialized.Aggregate);
            Assert.NotSame(@event, deserialized);
        }
    }
}