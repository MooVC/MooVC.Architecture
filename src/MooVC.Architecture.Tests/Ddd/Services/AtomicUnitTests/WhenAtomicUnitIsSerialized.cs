namespace MooVC.Architecture.Ddd.Services.AtomicUnitTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAtomicUnitIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();

            SerializableDomainEvent<SerializableAggregateRoot>[] events = new[]
            {
                new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
            };

            var original = new AtomicUnit(events);
            AtomicUnit deserialized = original.Clone();

            Assert.Equal(original.Id, deserialized.Id);
            Assert.Equal(original.Events, deserialized.Events);
            Assert.NotSame(original, deserialized);
        }
    }
}