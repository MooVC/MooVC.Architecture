namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventReconciliationEventArgsTests
{
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenEventReconciliationEventArgsIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(context, aggregate) };
            var original = new EventReconciliationEventArgs(events);
            EventReconciliationAsyncEventArgs deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Events, deserialized.Events);
        }
    }
}