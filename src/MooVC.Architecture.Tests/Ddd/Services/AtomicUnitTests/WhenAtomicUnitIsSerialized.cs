namespace MooVC.Architecture.Ddd.Services.AtomicUnitTests
{
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenAtomicUnitIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();
            SerializableDomainEvent[] events = new[]
            {
                new SerializableDomainEvent(context, aggregate.ToVersionedReference()),
            };

            var unit = new AtomicUnit(events);
            AtomicUnit deserialized = unit.Clone();

            Assert.Equal(unit.Id, deserialized.Id);
            Assert.Equal(unit.Events, deserialized.Events);
            Assert.NotSame(unit, deserialized);
        }
    }
}